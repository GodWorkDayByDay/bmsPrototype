using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class SceneManager
    {
        private static SceneManager instance;
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }
        xmlManager<Scene> xmlSceneManager;

        Scene currentScreen;
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SceneManager();
                return instance;
            }
        }

        public SceneManager()
        {
            Dimensions = new Vector2(640, 480);
            currentScreen = new SceneTitle();
            xmlSceneManager = new xmlManager<Scene>();
            xmlSceneManager.Type = currentScreen.Type;
            currentScreen = xmlSceneManager.Load("Load/SceneTitle.xml");
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
    }
}
