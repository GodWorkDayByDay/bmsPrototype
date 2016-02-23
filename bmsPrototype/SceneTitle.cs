using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace bmsPrototype
{

    public class Image
    {
        public string Path;
    }

    public class SceneTitle : Scene
    {
        Texture2D image;
        //[XmlElement("Path")]
        //public List<string> path;
        public Image Image;
        public Vector2 Position;

        public override void LoadContent()
        {
            base.LoadContent();
            image = content.Load<Texture2D>(Image.Path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, Color.White);
        }
    }
}
