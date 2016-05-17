using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmsPrototype
{
    public class BmsData
    {
        public int BarNumber;
        public int Channel;
        public float BarMagnitude; //for channel 02
        public Object[] Objects; //this isnt here??
        public bool IsCorrectData; //this is correct BMS data line
        /* private */
        private string data;
        private List<int> dataList;
        private bool debug = false;
        private int barCount = 10000;//magic number

        public BmsData(string line)
        {
            this.BarNumber = 0;
            this.Channel = 0;
            this.data = string.Empty;
            this.BarMagnitude = 1.0f;
            this.dataList = new List<int>();
            this.IsCorrectData = true;
            if (debug)
            {
                System.Console.WriteLine(line.Substring(0, 1) + " == # ?");
                System.Console.WriteLine(line.IndexOf(":") + " has : ?");
            }
            if (line.Substring(0, 1) == "#" && line.IndexOf(":") != 0) {
                string[] data = line.Split(':');
                this.BarNumber = Int32.Parse(data[0].Substring(1, 3));
                this.Channel = Int32.Parse(data[0].Substring(4, 2));
                this.data = data[1];
                InterpretBmsData();
                //printBmsData(); //for debug line
            }
            else
            {
                this.IsCorrectData = false;
            }
        }

        private void InterpretBmsData()
        {
            if (Channel == 1)
            {
                if (this.data == "00")
                {
                    this.IsCorrectData = false; //this is empty data
                }
            }
            else if (Channel == 2)//change bar count 
            {
                float f;
                if (float.TryParse(this.data, out f))
                {
                    this.BarMagnitude = float.Parse(this.data);
                }
            }
            else if(11 <= Channel && Channel <= 19) //1P Object
            {
                if(this.data.Length % 2 == 0)
                {
                    //TODO data is int?
                    this.dataList = substringAtCount(this.data, 2);
                }
            }
        }

        public void UpdateObjectsBarMagnitude(float barMagnitude)
        {
            if (this.Channel != 2)
            {
                this.BarMagnitude = barMagnitude;
                /* TODO calculate Object.Time */


            }
        }

        private List<int> substringAtCount(string source, int count)
        {
            List<int> substList = new List<int>();
            int length = (int)Math.Ceiling((double)source.Length / (double)count);
            int j;

            for(int i = 0; i < length; i++)
            {
                int start = count * i;
                if(start >= source.Length)
                {
                    break;
                }
                if(start + count > source.Length)
                {
                    if (Int32.TryParse(source.Substring(start), out j))
                        substList.Add(Int32.Parse(source.Substring(start)));
                }
                else
                {
                    if (Int32.TryParse(source.Substring(start), out j))
                        substList.Add(Int32.Parse(source.Substring(start, count)));
                }
            }
            return substList;
        }
        /// <summary>
        /// for debug
        /// </summary>
        public void printBmsData()
        {
            System.Console.WriteLine("CH: " + this.Channel.ToString());
            System.Console.WriteLine("BarNumber: " + this.BarNumber.ToString());
            System.Console.WriteLine("BarMagnitude: " + this.BarMagnitude.ToString());
            System.Console.WriteLine("data: " + this.data);
            if (this.Objects != null)
            {
                System.Console.WriteLine("Objects");
                int objcount = 0;
                foreach (Object obj in this.Objects)
                {
                    System.Console.WriteLine("Object: " + ++objcount);
                    System.Console.WriteLine("Time: " + obj.Time);
                    System.Console.WriteLine("Data: " + obj.Data);
                }
            }
            System.Console.WriteLine("--------------------");
        }
        /// <summary>
        /// calculate object time after set ch2 magnitude data
        /// </summary>
        public void calcObjectTime(int time)
        {
            int divideNum = this.dataList.Count();
            int num = 0;
            if (divideNum != 0)
            {
                foreach(int data in this.dataList)
                {
                    Object obj = new Object(time + num*(barCount/divideNum), data);
                    num++;
                }
            }
        }
    }
}
