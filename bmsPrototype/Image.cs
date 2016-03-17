using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace bmsPrototype
{
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        public bool IsActive;
        [XmlIgnore]
        public Texture2D Texture;
        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget;
        SpriteFont font;

        Dictionary<string, ImageEffect> effectList;
        public string Effects;

        public FadeEffect FadeEffect;
        public SpriteSheetEffect SpriteSheetEffect;

        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            effectList.Add(effect.GetType().ToString().Replace("bmsPrototype.", ""), (effect as ImageEffect));
            foreach (KeyValuePair<string, ImageEffect> kvp in effectList)
            {
                //System.Console.Write("{0:s}*{1:d}", kvp.Key, kvp.Value);
            }
        }

        public void ActivateEffect(string effect)
        {
            //System.Console.WriteLine("ActiveEffect is called. effect string:" + effect);
            //todo check this 
            if (effectList.ContainsKey(effect))
            {
                //System.Console.WriteLine(effectList[effect].ToString());
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);

            }
        }

        public void DeactiveEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnLoadContent();
            }
        }

        public void StoreEffects()
        {
            Effects = String.Empty;
            foreach(var effect in effectList)
            {
                if (effect.Value.IsActive)
                    Effects += effect.Key + ":";
            }
            if (Effects != String.Empty)
            Effects.Remove(Effects.Length - 1);
        }

        public void RestoreEffects()
        {
            foreach (var effect in effectList)
            {
                DeactiveEffect(effect.Key);
            }
            string[] split = Effects.Split(':');
            foreach(string s in split)
            {
                ActivateEffect(s);
            }
        }

        public Image()
        {
            Path = Text = Effects = String.Empty;
            FontName = "Fonts/Arial";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent()
        {
            content = new ContentManager(SceneManager.Instance.Content.ServiceProvider, "Content");

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);

            font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if (Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += font.MeasureString(Text).X;

            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            else
                dimensions.Y = font.MeasureString(Text).Y;

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(SceneManager.Instance.GraphicsDevice, 
                (int)dimensions.X, (int)dimensions.Y);
            SceneManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            SceneManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            SceneManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
                SceneManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            SceneManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);
            SceneManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            SceneManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref FadeEffect);
            SetEffect<SpriteSheetEffect>(ref SpriteSheetEffect);

            if (Effects != String.Empty)
            {
                //System.Console.WriteLine("activate effect:" + Effects.ToString());
                string[] split = Effects.Split(':');
                foreach (string item in split)
                {
                    //System.Console.WriteLine("activating");
                    ActivateEffect(item);
                }
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
            {
                //effect.Value.UnLoadContent ();
                DeactiveEffect(effect.Key);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                //System.Console.WriteLine ("foreach at image update"); //for debug
                if (effect.Value.IsActive)
                {
                    //System.Console.WriteLine ("active effect is here");//for debug
                    effect.Value.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha, 
                0.0f, origin, Scale, SpriteEffects.None, 0.0f);

        }
    }
}
