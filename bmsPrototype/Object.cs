using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace bmsPrototype
{
    public class Object
    {
        public int Time;
        public int Data;
        public Image Image;

        public Object(int time, int data)
        {
            this.Time = time;
            this.Data = data;
            this.Image = null;
        }

        public void LoadContent(string path, Vector2 scale, Vector2 position)
        {
            this.Image = new Image();
            this.Image.Path = path;
            this.Image.Scale = scale;
            this.Image.Position = position;
            this.Image.LoadContent();
        }

        public void UnLoadContent()
        {
            this.Image.UnloadContent();
        }
    }
}
