using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class SceneGameplay : Scene
    {
        Player player;

        public override void LoadContent()
        {
            base.LoadContent();
            xmlManager<Player> playerLoader = new xmlManager<Player>();
            player = playerLoader.Load("Load/Player.xml");
            player.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
