using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace bmsPrototype
{
    public class SceneTitle : Scene
    {
        Texture2D image;
        string path;

        public override void LoadContent()
        {
            base.LoadContent();
            path = "data/white";
            image = content.Load<Texture2D>(path);
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
            spriteBatch.Draw(image, Vector2.Zero, Color.White);
        }
    }
}
