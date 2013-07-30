using System;
using System.Collections.Generic;
using System.Drawing;

namespace ThreeSharp
{
	public class MeshBasicMaterial:Material
	{
		public Color color;
		public object lightMap;
		public object specularMap;
		public object envMap;
		public int combine;
		public int reflectivity;
		public float refractionRatio;
		public bool fog;
		public int shading;
		public bool wireframe;
		public int wireframeLineWidth;
		public string wireframeLinecap;
		public string wireframeLineJoin;
		public int vertexColors;
		public bool skinning;
		public bool morphTargets;



		public MeshBasicMaterial (Texture map=null)
		{
			this.color = (Color)(new ColorConverter()).ConvertFromString("#FFFFFF");

			this.map = null;

			this.lightMap = null;

			this.specularMap = null;

			this.envMap = null;
			this.combine = Three.MultiplyOperation;
			this.reflectivity = 1;
			this.refractionRatio = 0.98f;

			this.fog = true;

			this.shading = Three.SmoothShading;

			this.wireframe = false;
			this.wireframeLineWidth = 1;
			this.wireframeLinecap = "round";
			this.wireframeLineJoin = "round";

			this.skinning = false;
			this.morphTargets = false;

            if (map!=null)
            {
                this.setValues(map);
            }
		}
	}
}

