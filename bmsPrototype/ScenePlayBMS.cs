using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class ScenePlayBMS : Scene
    {
        PanelPlayer PanelPlayer;
        PanelInfo PanelInfo;

        public override void LoadContent()
        {
            base.LoadContent();
            xmlManager<PanelPlayer> panelPlayerLoader = new xmlManager<PanelPlayer>();
            PanelPlayer = panelPlayerLoader.Load("Load/PlayBMS/PanelPlayer.xml");
            xmlManager<PanelInfo> panelInfoLoader = new xmlManager<PanelInfo>();
            PanelInfo = panelInfoLoader.Load("Load/PlayBMS/PanelInfo.xml");
            /* Load Content */
            PanelPlayer.LoadContent();
            PanelInfo.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            PanelPlayer.UnloadContent();
            PanelInfo.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            PanelPlayer.Update(gameTime);
            PanelInfo.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            PanelPlayer.Draw(spriteBatch);
            PanelInfo.Draw(spriteBatch);
        }
    }
}
