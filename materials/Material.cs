using System;

namespace ThreeSharp
{
	public class Material
	{
		public static int MaterialIdCount = 0;

		public int id;
		public string name;
		public int side;
		public int opacity;
		public bool transparent;
		public int blending;
		public int blendSrc;
		public int blendDst;
		public int blendEquation;
		public bool depthTest;
		public bool depthWrite;
		public bool polygonOffset;
		public int polygonOffsetFactor;
		public int polygonOffsetUnits;
		public int alphaTest;
		public bool overdraw;
		public bool visible;
		public bool needsUpdate;


		public Texture map;

		public Material ()
		{
			this.id = MaterialIdCount++;

			this.name = "";

			this.side = Three.FrontSide;

			this.opacity = 1;
			this.transparent = false;

			this.blending = Three.NormalBlending;

			this.blendSrc = Three.SrcAlphaFactor;
			this.blendDst = Three.OneMinusDstAlphaFactor;
			this.blendEquation = Three.AddEquation;

			this.depthTest = true;
			this.depthWrite = true;

			this.polygonOffset = false;
			this.polygonOffsetFactor = 0;
			this.polygonOffsetUnits = 0;

			this.alphaTest = 0;

			this.overdraw = false; // Boolean for fixing antialiasing gaps in CanvasRenderer

			this.visible = true;

			this.needsUpdate = true;

		}



		public void setValues(Texture map)
		{
			if(map == null) 
				return;

			this.map = map;
		}
	}
}

