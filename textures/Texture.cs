using System;

namespace ThreeSharp
{
	public class Texture
	{
		public Texture ()
		{
		}

		float anisotropy;

		public float Anisotropy {
			get {
				return anisotropy;
			}
			set {
				anisotropy = value;
			}
		}
	}
}

