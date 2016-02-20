using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using System;
//using System.IO;


namespace bmsPrototype
{
    /// <summary>
    /// This is the main type for your game..
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Scene scene;
        SceneTitle title = new SceneTitle();//todo

        Texture2D texture; //to write texture
        Vector2 pos; //position of texture

        public enum SCENE
        {
            TITLE,
            MODESEL,
            MUSICSEL,
            GAMEMAIN,
            RESULT,
        };

        /// <summary>
        /// constructor
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferMultiSampling = false; //antialiasing(multisampling)
            graphics.IsFullScreen = false; //fullscreen
            this.IsMouseVisible = true; //mouse visible
            this.pos.X = 0;
            this.pos.Y = 100;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Window.Title = "Mono BMS Prototype";
            changeScene(SCENE.TITLE);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// Load graphics
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //FileStream file = System.IO.File.OpenRead("C:\\Users\\taniguchi\\Documents\\Visual Studio 2015\\Projects\\bmsPrototype\\bmsPrototype\\data\\test.png");
            // this.texture = Texture2D.FromStream(this.GraphicsDevice, file);
            this.texture = Content.Load<Texture2D>("data/test");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// Unload graphics
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Left)) pos.X -= 4;
            if (keyState.IsKeyDown(Keys.Right)) pos.X += 4;
            if (keyState.IsKeyDown(Keys.Up)) pos.Y -= 4;
            if (keyState.IsKeyDown(Keys.Down)) pos.Y += 4;

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(this.pos.X, this.pos.Y), Color.White);
            //spriteBatch.Draw(texture, new Vector2(0.0f, 200.0f), Color.Green);
            //spriteBatch.Draw(texture, new Vector2(0.0f, 400.0f), new Color(0x80, 0xFF, 0x80));
            spriteBatch.End();
            this.scene.Draw();

            base.Draw(gameTime);
        }

        void changeScene(SCENE s)
        {
            
            switch(s)
            {
                case SCENE.TITLE:
                    this.scene = this.title;
                    break;
                default:
                    break;
            }
        }
    }
}
