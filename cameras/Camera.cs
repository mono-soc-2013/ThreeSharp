using System;
using OpenTK;

namespace ThreeSharp
{
	public class Camera: Object3D
	{
		public Matrix4 matrixWorldInverse;
		public Matrix4 projectionMatrix;
		public Matrix4 projectionMatrixInverse;

		public Camera ()
		{
			matrixWorldInverse = new Matrix4();
			projectionMatrix = new Matrix4();
			projectionMatrixInverse = new Matrix4();


		}
	}
}

