using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bmsPrototype
{
    public class Tune
    {   
        /* Headers */
        public string Path;
        public int Player;
        public string Genre;
        public string Title;
        public string Artist;
        public float Bpm;//initial BPM
        public int PlayLevel;
        public int Rank;
        public int VolumeWav;
        public float Total;
        public string StageFilePath;
        public int Difficulty;
        /* Tune Object */
        public Dictionary<int, Wav> WavDictionary;
        public List<BmsData> ListBmsData;
        public List<BmsData> ListBmsDataCh2;
        /* Tune State */
        public bool IsLoading { get; private set; }

        public Tune(String path)
        {
            Path = path;
            Genre = Title = Artist = StageFilePath = string.Empty;
            Player = PlayLevel = Rank = VolumeWav = Difficulty = 0;
            Bpm = Total = 0.0f;
            IsLoading = false;
            WavDictionary = new Dictionary<int, Wav>();
            ListBmsData = new List<BmsData>();
            ListBmsDataCh2 = new List<BmsData>();
            if (System.IO.File.Exists(Path))
            {
                System.Console.WriteLine("This BMS file exists:" + this.Path);
                LoadBMSFileAsync(new Progress<string>(line => System.Console.Write(line + System.Environment.NewLine)));
                System.Console.WriteLine("Loading.");
            }
            else
            {
                System.Console.WriteLine("This BMS file doesn't exists:" + this.Path);
            }
        }

        private void LoadBMSFile()
        {
            IsLoading = true;
            System.IO.StreamReader sr
                = new System.IO.StreamReader(Path, System.Text.Encoding.Default);
            string result = string.Empty;
            while(sr.Peek() >= 0)
            {
                string buffer = sr.ReadLine();
                System.Console.WriteLine(buffer);
                result += buffer + System.Environment.NewLine;
            }
            sr.Close();
        }

        private async void LoadBMSFileAsync(IProgress<string> progress)
        {
            await Task.Run(async () =>
            {
                System.Console.WriteLine("Load start.");
                IsLoading = true;
                System.IO.FileStream fs
                    = new System.IO.FileStream(Path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                System.IO.StreamReader sr
                    = new System.IO.StreamReader(fs);
                string result = string.Empty;
                string buffer = await sr.ReadLineAsync();
                while (buffer != null)
                {
                    //progress.Report(buffer);
                    System.Console.WriteLine(buffer);
                    buffer.Replace("\n", "").Replace("\r", "");
                    if (buffer != string.Empty)
                        InterpretCommand(buffer);
                    result += buffer + System.Environment.NewLine;
                    buffer = await sr.ReadLineAsync();
                }
                sr.Close();
                /* set bmsData magnitude */
                foreach(BmsData source in ListBmsDataCh2) //error may happen because list is not initialized
                {
                    foreach (BmsData target in ListBmsData)
                    {
                        if (target.BarNumber == source.BarNumber)
                        {
                            target.BarMagnitude = source.BarMagnitude;
                        }
                    }
                }
                /* calc time */
                /*
                int bar = 0;
                int time = 0;
                ListBmsData.Sort((a, b) => a.BarNumber - b.BarNumber);
                foreach(BmsData tmp in ListBmsData)
                {
                    if (bar == tmp.BarNumber)
                    {
                        tmp.calcObjectTime(time);
                    }
                    else {
                        time = tmp.calcObjectTime(time);
                        bar++;
                    }
                }
                */
                /* check bms data */
                foreach(BmsData tmp in ListBmsData)
                {
                    if(tmp.BarNumber == 0)
                        tmp.printBmsData();
                }
                /* check wav data */
                /*
                foreach(KeyValuePair<int, Wav> dic in WavDictionary)
                {
                    System.Console.WriteLine("Wav{0}:{1}, {2}", dic.Key, dic.Value.FileName, dic.Value.Number); 
                }
                */
                IsLoading = false;
            }
            );
        }

        private void InterpretCommand(string line)
        {            
            line.Trim();//trim spases from command's start and end.
            string[] commandArray = line.Split(' ');
            if (commandArray.Length > 2)
                for (int i = 2; i<commandArray.Length; i++)
                {
                    commandArray[1] += " " + commandArray[i];
                }
            /*
            foreach(string str in commandArray)
            {
                System.Console.WriteLine(str);
            }
            */
            int j = 0;
            float f = 0.0f;
            if (line.Substring(0, 1) == "#")
                switch(line.Substring(1, 1))
                {
                    case "A":
                        if (commandArray[0] == "#ARTIST")
                        {
                            this.Artist = commandArray[1];
                        }
                        break;
                    case "B":
                        if(commandArray[0] == "#BPM")
                        {
                            if(float.TryParse(commandArray[0], out f))
                            { 
                                System.Console.WriteLine("complete cast float.");
                                this.Bpm = f;
                            }
                        }
                        break;
                    case "D":
                        if(commandArray[0] == "#DIFFICULTY")
                        {
                            if(Int32.TryParse(commandArray[0], out j))
                            {
                                System.Console.WriteLine("complete cast int.");
                                this.Difficulty = j;
                            }
                        }
                        break;
                    case "G":
                        if(commandArray[0] == "#GENRE")
                        {
                            this.Genre = commandArray[1];
                        }
                        break;
                    case "P":
                        if (commandArray[0] == "#PLAYER")
                        {
                            if (Int32.TryParse(commandArray[1], out j))
                            {
                                System.Console.WriteLine("complete cast int.");
                                this.Player = j;
                            }
                            else
                                System.Console.WriteLine("couldn't cast int.");
                        }
                        else if (commandArray[0] == "#PLAYLEVEL")
                        {
                            if (Int32.TryParse(commandArray[1], out j))
                            { 
                                System.Console.WriteLine("complete cast int.");
                                this.PlayLevel = j;
                            }
                            else
                                System.Console.WriteLine("couldn't cast int.");
                        }
                        break;
                    case "R":
                        if (commandArray[0] == "#RANK")
                        {
                            if (Int32.TryParse(commandArray[1], out j))
                            {
                                System.Console.WriteLine("complete cast int.");
                                this.Rank = j;
                            }
                            else
                                System.Console.WriteLine("couldn't cast int.");
                        }
                        break;
                    case "S":
                        if (commandArray[0] == "#STAGEFILE")
                        {
                            this.StageFilePath = commandArray[1];
                            if (System.IO.File.Exists(this.StageFilePath))
                            {
                                System.Console.WriteLine("This stagefile exists.");
                            }
                            else
                            {
                                System.Console.WriteLine("This stagefile doesn't exist.");
                                this.StageFilePath = string.Empty;
                            }
                        }
                        break;
                    case "T":
                        if (commandArray[0] == "#TITLE")
                        {
                            this.Title = commandArray[1];
                        }
                        else if(commandArray[0] == "#TOTAL")
                        {
                            if (Int32.TryParse(commandArray[1], out j))
                            {
                                System.Console.WriteLine("complete cast int.");
                                this.Total = j;
                            }
                            else
                                System.Console.WriteLine("couldn't cast int.");
                        }
                        break;
                    case "U":
                        if (commandArray[0] == "GENRE")
                        {

                        }
                        break;
                    case "W":
                        if(commandArray[0].Substring(0, 4) == "#WAV")
                        {
                            string wavNumberStr = commandArray[0].Substring(4, 2);
                            int wavNumberInt = RadixConvert.ToInt16(wavNumberStr, 36);
                            Wav wav = new Wav(commandArray[1], wavNumberInt);
                            WavDictionary.Add(wavNumberInt, wav);
                        }
                        break;
                    default:
                        //System.Console.Write("default");
                        //System.Console.WriteLine(line.Substring(1, line.Length - 1));
                        //System.Console.WriteLine(line.Remove('#'));
                        BmsData bmsData = new BmsData(line);
                        if (bmsData.IsCorrectData)
                        {
                            if (bmsData.Channel == 2)
                            {
                                ListBmsDataCh2.Add(bmsData); //need to change magnitude or not 
                            }
                            else
                            {
                                ListBmsData.Add(bmsData);
                            }
                        }
                        /*
                        string[] data = line.Split(':');
                        data[0].Substring(1, 3);
                        data[0].Substring(4, 2);
                        */
                        break;
                }
     
        }
    }
}
