using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class PanelPlayer
    {
        public Vector2 Position;
        public Image Base, Lane, LaneCover, JudgeLine;
        public Image Button, TurnTable;

        public PanelPlayer()
        {
            Position = Vector2.Zero;
        }

        public void LoadContent()
        {
            Base.LoadContent();
            Lane.LoadContent();
            Base.Position = Position + Base.Position;
            Lane.Position = Position + Lane.Position;
        }

        public void UnloadContent()
        {
            Base.UnloadContent();
            Lane.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Base.Update(gameTime);
            Lane.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Base.Draw(spriteBatch);
            Lane.Draw(spriteBatch);
        }
    }
}
