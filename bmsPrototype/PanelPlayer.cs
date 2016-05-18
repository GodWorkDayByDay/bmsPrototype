using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace bmsPrototype
{
    public class PanelPlayer
    {
        public Vector2 Position;
        public Image Base, Lane, LaneCover, JudgeLine;
        public Image Button, TurnTable;
        [XmlArray("NoteXList")]
        [XmlArrayItem("CustomItem")]
        public List<int> NoteXList { get; set; }
        [XmlIgnore]
        public List<Object> WhiteNoteList, BlueNoteList, RedNoteList;
        public List<int> AssignWhiteNote, AssignBlueNote, AssignRedNote;

        public PanelPlayer()
        {
            Position = Vector2.Zero;
            WhiteNoteList = new List<Object>();
            BlueNoteList = new List<Object>();
            RedNoteList = new List<Object>();
            AssignWhiteNote = new List<int> { 11, 13, 15, 17 };
            AssignBlueNote = new List<int> { 12, 14, 16 };
            AssignRedNote = new List<int> { 10, 18, 19 };
            /*
            NoteXList = new List<int>();
            foreach(int i in NoteXList)
            {
                System.Console.WriteLine(i);
            }
            */
        }

        public void LoadContent()
        {
            Base.LoadContent();
            Lane.LoadContent();
            Base.Position = Position + Base.Position;
            Lane.Position = Position + Lane.Position; 
        }

        public void UnloadContent()
        {
            Base.UnloadContent();
            Lane.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Base.Update(gameTime);
            Lane.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Base.Draw(spriteBatch);
            Lane.Draw(spriteBatch);
        }
        /*
        public void LoadcontentNoteList(int gameCount, BmsData bmsData)
        {
            switch (bmsData.Channel)
            {
                case 10:
                    break;
                case 11:
                    foreach (Object obj in bmsData.Objects)
                    {
                        if (gameCount < obj.Time && obj.Time < gameCount + 10000)
                        {
                            Image whiteNote = new Image();
                            whiteNote.Path = "PlayBMS/White";
                            whiteNote.Scale = new Vector2(20, 5);
                            whiteNote.Position = new Vector2(80, 400 - (obj.Time - gameCount)/32);
                            whiteNote.LoadContent();
                            WhiteNoteList.Add(whiteNote);
                        }
                    }
                    break;
                default: break;
            }
        }
        */

        public void UpdateNoteList(int gameCount, Tune tune)
        {
            foreach(BmsData bmsData in tune.ListBmsData)
            {
                if(10 <= bmsData.Channel && bmsData.Channel <= 19) //if 1P channel
                {
                    int ch = bmsData.Channel;
                    int key = 0;
                    int noteX = 0;
                    string path = "";
                    Vector2 scale = new Vector2(0, 0);
                    if (AssignWhiteNote.Contains(ch))
                    {
                        key = 1 + 2 * AssignWhiteNote.IndexOf(ch);
                        path = "PlayBMS/White";
                        scale = new Vector2(22, 5);
                    }
                    else if (AssignBlueNote.Contains(ch))
                    {
                        key = 2 * (AssignBlueNote.IndexOf(ch) + 1);
                        path = "PlayBMS/Blue";
                        scale = new Vector2(17, 5);
                    }
                    else if (AssignRedNote.Contains(ch))
                    {
                        key = AssignRedNote.IndexOf(ch) * 0;
                        path = "PlayBMS/Red";
                        scale = new Vector2(41, 5);
                    }
                    noteX = (int)Lane.Position.X + NoteXList[key];
                    foreach (Object obj in bmsData.Objects)
                    {
                        if (obj.Time < gameCount)
                        {
                            if (WhiteNoteList.Contains(obj))
                            {
                                obj.Image.UnloadContent();
                                WhiteNoteList.Remove(obj);
                            }
                            else if (BlueNoteList.Contains(obj))
                            {
                                obj.Image.UnloadContent();
                                BlueNoteList.Remove(obj);
                            }
                            else if (RedNoteList.Contains(obj))
                            {
                                obj.Image.UnloadContent();
                                RedNoteList.Remove(obj);
                            }
                        }
                        else if (gameCount <= obj.Time && obj.Time <= gameCount + 10000)
                        {
                            Vector2 position = new Vector2(noteX, Lane.Position.Y + Lane.Texture.Height - (obj.Time - gameCount) / 32);
                            if (AssignWhiteNote.Contains(ch) && !WhiteNoteList.Contains(obj)) {
                                obj.LoadContent(path, scale, position);
                                WhiteNoteList.Add(obj);
                            }
                            else if (AssignBlueNote.Contains(ch) && !BlueNoteList.Contains(obj))
                            {
                                obj.LoadContent(path, scale, position);
                                BlueNoteList.Add(obj);
                            }
                            else if (AssignRedNote.Contains(ch) && !RedNoteList.Contains(obj))
                            {
                                obj.LoadContent(path, scale, position);
                                RedNoteList.Add(obj);
                            }
                            else
                            {
                                obj.Image.Position = position;
                            }
                        }
                    }
                }
            }
        }

        public void DrawNoteList(SpriteBatch spriteBatch)
        {
            foreach (Object whiteNote in WhiteNoteList)
            {
                whiteNote.Image.Draw(spriteBatch);
            }
            foreach (Object blueNote in BlueNoteList)
            {
                blueNote.Image.Draw(spriteBatch);
            }
            foreach (Object redNote in RedNoteList)
            {
                redNote.Image.Draw(spriteBatch);
            }
        }
    }
}
