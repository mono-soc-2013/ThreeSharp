using System;
using System.Collections.Generic;

namespace ThreeSharp
{
	public class MeshBasicMaterial:Material
	{
		public MeshBasicMaterial (Texture map)
		{
			this.setValues(map);
		}
	}
}

