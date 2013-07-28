using System;

namespace ThreeSharp
{
	public class Fog
	{
		public string name;
		public string hex;
		public int near;
		public int far;

		public Fog (string hex,int near = 1,int far =1000)
		{
			this.name = "";
			this.hex = hex;
			this.near = near;
			this.far = far;
		}
	}
}

