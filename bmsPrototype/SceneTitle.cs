using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace bmsPrototype
{
    public class SceneTitle : Scene
    {
        public Image Image;
        public Image TextPress;

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            System.Console.WriteLine("Image IsActive:" + Image.IsActive.ToString());
            System.Console.WriteLine("FadeEffect IsActive:" + Image.FadeEffect.IsActive.ToString());
            TextPress.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
            TextPress.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
            TextPress.Update(gameTime);
            //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !SceneManager.Instance.IsTransitioning) //this is old
            if(InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
                SceneManager.Instance.CangeScreens("SceneMenu");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
            TextPress.Draw(spriteBatch);
        }
    }
}
