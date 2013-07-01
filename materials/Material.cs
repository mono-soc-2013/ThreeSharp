using System;

namespace ThreeSharp
{
	public class Material
	{
		public Material ()
		{
		}

		int blending;
		Texture map;

		public void setValues(Texture map)
		{
			this.map = map;
		}
	}
}

