using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace bmsPrototype
{
    public class Scene
    {
        protected ContentManager content;
        [XmlIgnore]
        public Type Type;
        public string xmlPath;


        public Scene()
        {
            Type = this.GetType();
            xmlPath = "Load/" + Type.ToString().Replace("bmsPrototype.", "") + ".xml";
            System.Console.WriteLine("Scene Constructor caled : " + xmlPath);
        }
   
        public virtual void LoadContent()
        {
            content = new ContentManager(SceneManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
        
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
