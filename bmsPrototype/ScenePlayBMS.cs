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
        Tune Tune;

        private string loadStartTime, loadEndTime, gameCountStr;
        private int gameCount;

        public override void LoadContent()
        {
            /* init values */
            loadStartTime = loadEndTime = string.Empty;
            this.gameCount = 0;
            /* for Load Content */
            xmlManager<PanelPlayer> panelPlayerLoader = new xmlManager<PanelPlayer>();
            PanelPlayer = panelPlayerLoader.Load("Load/PlayBMS/PanelPlayer.xml");
            xmlManager<PanelInfo> panelInfoLoader = new xmlManager<PanelInfo>();
            PanelInfo = panelInfoLoader.Load("Load/PlayBMS/PanelInfo.xml");
            /* Load Content */
            base.LoadContent();
            PanelPlayer.LoadContent();
            PanelInfo.LoadContent();
            Tune = new Tune("./Content/indication_ogg/indication_normal.bme");
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

            if (loadStartTime == string.Empty)
                loadStartTime = "LST: " + gameTime.TotalGameTime.ToString();
            if (Tune.IsLoading)
                PanelInfo.Loading.IsActive = true;
            else
            {
                if (loadEndTime == string.Empty)
                    loadEndTime = "LET: " + gameTime.TotalGameTime.ToString();
                PanelInfo.Loading.IsActive = false;
                PanelInfo.Loading.Alpha = 0.0f;
                /* tune start */
                gameCount += 80; //todo correct increment value
                PanelPlayer.UpdateNoteList(gameCount, this.Tune);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            PanelPlayer.Draw(spriteBatch);
            if (!Tune.IsLoading)
            {
                PanelPlayer.DrawNoteList(spriteBatch);
            }
            PanelInfo.Draw(spriteBatch);
            PanelInfo.DrawText(spriteBatch, loadStartTime, 4);
            PanelInfo.DrawText(spriteBatch, loadEndTime, 5);
            PanelInfo.DrawText(spriteBatch, "#PLAYER: " + Tune.Player.ToString(), 6);
            PanelInfo.DrawText(spriteBatch, "#PLAYLEVEL: " + Tune.PlayLevel.ToString(), 7);
            PanelInfo.DrawText(spriteBatch, "#ARTIST: " + Tune.Artist, 8);
            PanelInfo.DrawText(spriteBatch, "#TITLE: " + Tune.Title, 9);
            PanelInfo.DrawText(spriteBatch, "#GENRE: " + Tune.Genre, 10);
            PanelInfo.DrawText(spriteBatch, "#BPM: " + Tune.Bpm.ToString(), 11);
            PanelInfo.DrawText(spriteBatch, "#RANK: " + Tune.Rank.ToString(), 12);
            PanelInfo.DrawText(spriteBatch, "gameCount: " + this.gameCount.ToString(), 13);
        }
    }
}
