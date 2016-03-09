using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class SceneManager
    {
        private static SceneManager instance;
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        xmlManager<Scene> xmlSceneManager;

        Scene currentScreen, newScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;

        public Image Image;
        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        public void CangeScreens(string screenName)
        {
            System.Console.WriteLine("changeScreen called:" + screenName);
            newScreen = (Scene)Activator.CreateInstance(Type.GetType("bmsPrototype." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
            System.Console.WriteLine("changing scene");
        }

        public void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                Image.Update(gameTime);
                if (Image.Alpha == 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    xmlSceneManager.Type = currentScreen.Type;
                    if (File.Exists(currentScreen.xmlPath))
                    {
                        currentScreen = xmlSceneManager.Load(currentScreen.xmlPath);
                    }
                    currentScreen.LoadContent();
                }
                else if (Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    xmlManager<SceneManager> xml = new xmlManager<SceneManager>();
                    instance = xml.Load("Load/SceneManager.xml");
                    //instance = new SceneManager(); //this is old
                }
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
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
            {
                Image.Draw(spriteBatch);
            }
        }
    }
}
