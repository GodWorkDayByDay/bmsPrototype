using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class PanelInfo
    {
        public Vector2 Position;
        public Image Base, Fps, Loading;
        FrameCounter frameCounter;
        ContentManager content;
        SpriteFont font;
        string fps, gameTime, eGameTime;

        public PanelInfo()
        {
            Position = Vector2.Zero;
            frameCounter = new FrameCounter();
            fps = "0";
            gameTime = eGameTime = string.Empty;
        }

        public void DrawText(SpriteBatch spriteBatch, string str, int lineNum)
        {
            spriteBatch.DrawString(font, str, new Vector2(Fps.Position.X, Fps.Position.Y + Fps.Texture.Height * lineNum), Color.White);
        }

        public void LoadContent()
        {
            Base.LoadContent();
            Fps.LoadContent();
            Loading.LoadContent();
            Base.Position = Position + Base.Position;
            Fps.Position = Position + Fps.Position;
            Loading.Position = new Vector2(Fps.Position.X, Fps.Position.Y + Fps.Texture.Height * 3);

            string FontName = Fps.FontName;
            content = new ContentManager(SceneManager.Instance.Content.ServiceProvider, "Content");
            font = content.Load<SpriteFont>(FontName);
        }

        public void UnloadContent()
        {
            Base.UnloadContent();
            Fps.UnloadContent();
            Loading.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            /* some of fps counter */
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter.Update(deltaTime);
            fps = string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond);
            this.gameTime = "TGT: " + gameTime.TotalGameTime.ToString();
            this.eGameTime = "EGT: " + gameTime.ElapsedGameTime.ToString();
            /* Update */
            Base.Update(gameTime);
            Fps.Update(gameTime);
            Loading.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            /* Draw */
            Base.Draw(spriteBatch);
            //Fps.Draw(spriteBatch);
            Loading.Draw(spriteBatch);
            spriteBatch.DrawString(font, fps, Fps.Position, Color.White);
            spriteBatch.DrawString(font, gameTime, new Vector2(Fps.Position.X, Fps.Position.Y + Fps.Texture.Height * 1), Color.White);
            spriteBatch.DrawString(font, eGameTime, new Vector2(Fps.Position.X, Fps.Position.Y + Fps.Texture.Height * 2), Color.White);
        }
    }
}
