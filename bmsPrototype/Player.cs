using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace bmsPrototype
{
    class Player
    {
        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed;

        public Player()
        {
            Velocity = Vector2.Zero;

        }

        public void LoadContent()
        {
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (Velocity.X == 0){
                if (InputManager.Instance.KeyDown(Keys.Down))
                    Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else if (InputManager.Instance.KeyDown(Keys.Up))
                    Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    Velocity.Y = 0;
            }
            if (Velocity.Y == 0) {
                if (InputManager.Instance.KeyDown(Keys.Right))
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else if (InputManager.Instance.KeyDown(Keys.Left))
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    Velocity.X = 0;
            }
            Image.Position += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
