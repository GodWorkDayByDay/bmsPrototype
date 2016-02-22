using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class Scene
    {
        protected ContentManager content;

        public Scene()
        {

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
