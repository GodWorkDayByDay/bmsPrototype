using System;
using Microsoft.Xna.Framework;

namespace bmsPrototype
{
	public class ImageEffect
	{
		protected Image image;
		public bool IsActive;

		public ImageEffect ()
		{
			IsActive = false;
		}

		public virtual void LoadContent(ref Image Image)
		{
			this.image = Image;
		}

		public virtual void UnLoadContent()
		{

		}

		public virtual void Update(GameTime gameTime)
		{
			
		}
	}
}

