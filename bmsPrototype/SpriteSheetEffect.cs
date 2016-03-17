﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class SpriteSheetEffect : ImageEffect
    {
        public int FrameCounter;
        public int SwitchFrame;
        public Vector2 CurrentFrame;
        public Vector2 AmouontOfFrames;

        public int FrameWidth
        {
            get
            {
                if (image.Texture != null)
                    return image.Texture.Width / (int)AmouontOfFrames.X;
                return 0;
            }
        }

        public int FrameHeight
        {
            get
            {
                if (image.Texture != null)
                    return image.Texture.Height / (int)AmouontOfFrames.Y;
                return 0;
            }
        }

        public SpriteSheetEffect()
        {
            CurrentFrame = new Vector2(1, 0);
            AmouontOfFrames = new Vector2(3, 4);
            SwitchFrame = 100;
            FrameCounter = 0;
        }

        public override void LoadContent(ref Image Image)
        {
            base.LoadContent(ref Image);
        }

        public override void UnLoadContent()
        {
            base.UnLoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(image.IsActive)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    CurrentFrame.X++;
                    if (CurrentFrame.X * FrameWidth >= image.Texture.Width)
                        CurrentFrame.X = 0;
                }
            }
            else
            {
                CurrentFrame.X = 1;
            }
            image.SourceRect = new Rectangle((int)CurrentFrame.X * FrameWidth, (int)CurrentFrame.Y * FrameHeight, FrameWidth, FrameHeight);
        }
    }
}
