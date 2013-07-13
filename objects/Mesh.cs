using System;

namespace ThreeSharp
{
	public class Mesh: Object3D
	{
		public Geometry geometry;
		public Material material;

		public Mesh (Geometry geometry,Material material)
		{
			this.geometry = null;
			this.material = null;

		}
	}
}

