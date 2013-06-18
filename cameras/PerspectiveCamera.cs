using System;

namespace ThreeSharp
{
	public class PerspectiveCamera
	{
		int fav;
		float aspect;
		float near;
		float far;


		public PerspectiveCamera (int fav, float aspect, float near, float far)
		{
			this.fav = fav;
			this.aspect = aspect;
			this.near = near;
			this.far = far;
		}


	}
}

