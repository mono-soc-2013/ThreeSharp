using System;

namespace ThreeSharp
{
	public class PerspectiveCamera
	{
		float fav;
		float aspect;
		float near;
		float far;
		float x_possition=0.0f,y_possition=0.0f,z_possition=0.0f;

		public float X_POSSITION {
			get {
				return x_possition;
			}
			set {
				x_possition = value;
			}
		}

		public float Y_POSSITION {
			get {
				return y_possition;
			}
			set {
				y_possition = value;
			}
		}

		public float Z_POSSITION {
			get {
				return z_possition;
			}
			set {
				z_possition = value;
			}
		}
		

		public PerspectiveCamera (float fav, float aspect, float near, float far)
		{
			this.fav = fav;
			this.aspect = aspect;
			this.near = near;
			this.far = far;
		}




	}
}

