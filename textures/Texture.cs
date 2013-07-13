using System;
using System.Drawing;
using OpenTK;
using System.Collections.Generic;

namespace ThreeSharp
{
	public class Texture
	{
		public static int TextureIdCount = 0;
		public int id = 0;
		public string name;
		public Image image;
		public Three.UVMapping mapping;
		public List<int> mipmaps;
		public int wrapS;
		public int wrapT;
		public int magFilter;
		public int minFilter;
		public int anisotropy;
		public int format;
		public int type;
		public Vector2 offset;
		public Vector2 repeat;

		public bool generateMipmaps;
		public bool premultiplyAlpha;
		public bool flipY;
		public int unpackAlignment; 

		public bool needsUpdate;
		public object onUpdate;

		public string sourceFile;

		public Texture (Image image = null,
		                Three.UVMapping mapping = null, 
		                int wrapS = Three.ClampToEdgeWrapping, 
		                int wrapT = Three.ClampToEdgeWrapping, 
		                int magFilter = Three.LinearFilter, 
		                int minFilter = Three.LinearMipMapLinearFilter, 
		                int format = Three.RGBAFormat,
		                int type = Three.UnsignedByteType, 
		                int anisotropy = 1)
		{

			this.id = TextureIdCount++;

			this.name = "";

			this.image = image;
			this.mipmaps = new List<int>();

			this.mapping = (mapping !=null?mapping:new Three.UVMapping());

			this.wrapS = wrapS;
			this.wrapT = wrapT;

			this.magFilter = magFilter;
			this.minFilter = minFilter;

			this.anisotropy = anisotropy;

			this.format = format;
			this.type = type;

			this.offset = new Vector2(0,0);
			this.repeat = new Vector2(1,1);

			this.generateMipmaps = true;
			this.premultiplyAlpha = false;
			this.flipY = true;
			this.unpackAlignment = 4; // valid values: 1, 2, 4, 8 (see http://www.khronos.org/opengles/sdk/docs/man/xhtml/glPixelStorei.xml)

			this.needsUpdate = false;
			this.onUpdate = null;
		}



		public Texture clone(Texture texture)
		{
			texture = (texture!=null?texture:new Texture());
			texture.image = this.image;
			texture.mipmaps = new List<int>(this.mipmaps);

			texture.mapping = this.mapping;

			texture.wrapS = this.wrapS;
			texture.wrapT = this.wrapT;

			texture.magFilter = this.magFilter;
			texture.minFilter = this.minFilter;

			texture.anisotropy = this.anisotropy;

			texture.format = this.format;
			texture.type = this.type;

			texture.offset = new Vector2(this.offset);
			texture.repeat = new Vector2(this.repeat);

			texture.generateMipmaps = this.generateMipmaps;
			texture.premultiplyAlpha = this.premultiplyAlpha;
			texture.flipY = this.flipY;
			texture.unpackAlignment = this.unpackAlignment;

			return texture;
		}



	}
}

