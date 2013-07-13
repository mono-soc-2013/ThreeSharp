using System;
using System.Drawing;

namespace ThreeSharp
{
	public class ImageUtils
	{
		public string crossOrigin = "anonymous";
		public event EventHandler OnLoad;
		public Texture texture;
		public Image image;

		public ImageUtils ()
		{
			OnLoad +=new EventHandler(onLoad);
		}


		public  Texture loadTexture (string url, Three.UVMapping mapping=null)
		{
			image = null;
			texture = new Texture (image, mapping);

			load (ref image, url);

			if (OnLoad != null) {
				OnLoad(this,EventArgs.Empty);
			}
			texture.sourceFile = url;

			return texture;
		}

		public void onLoad (object sender, EventArgs e)
		{
			ImageUtils retriever = (ImageUtils)sender;
			retriever.texture.image = retriever.image;
			retriever.texture.needsUpdate = true;
		}

		public void load (ref Image image, string url)
		{

			image = Image.FromFile(url);
		}
	}
}

