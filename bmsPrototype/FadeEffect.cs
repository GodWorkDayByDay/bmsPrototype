using System;
using Microsoft.Xna.Framework;
namespace bmsPrototype
{
	public class FadeEffect : ImageEffect
	{
		public float FadeSpeed;
		public bool Increase;
		
		public FadeEffect ()
		{
			System.Console.WriteLine ("fade effect called");
			System.Console.WriteLine ("IsActive:" + this.IsActive.ToString());
			FadeSpeed = 1;
			Increase = false;
		}

		public override void LoadContent (ref Image Image)
		{
			base.LoadContent (ref Image);
		}

		public override void UnLoadContent ()
		{
			base.UnLoadContent ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			//System.Console.WriteLine("FadeEffect Alpha:" + image.Alpha.ToString());
			if (image.IsActive) {
				if (!Increase) {
					image.Alpha -= FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
				} else {
					image.Alpha += FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
				}
				if (image.Alpha < 0.0f) {
					Increase = true;
					image.Alpha = 0.0f;
				} else if (image.Alpha > 1.0f) {
					Increase = false;
					image.Alpha = 1.0f;
				}
			} else {
				image.Alpha = 1.0f;
			}
		}
	}
}

