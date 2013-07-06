using System;
using OpenTK;

namespace ThreeSharp
{
	public class PerspectiveCamera: Camera
	{
		float fov;
		float aspect;
		float near;
		float far;


		float fullWidth;
		float fullHeight;
		float x;
		float y;
		float width;
		float height;

		public PerspectiveCamera (float fov = 50.0f, float aspect = 50.0f, float near = 0.1f, float far =2000.0f)
		{
			this.fov = fov;
			this.aspect = aspect;
			this.near = near;
			this.far = far;

		}

		public void setLens (float focalLength, float frameHeight)
		{
			if(frameHeight==null)
				frameHeight = 24.0f;

			this.fov = 2* (float) Math.radToDeg(System.Math.Atan((double)(frameHeight/(focalLength*2))));
			this.updateProjectionMatrix();

		}

		public void setViewOffset (float fullWidth, float fullHeight, float x, float y, float width, float height)
		{
			this.fullHeight = fullHeight;
			this.fullWidth = fullWidth;
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.updateProjectionMatrix();

		}

		public void updateProjectionMatrix ()
		{
			if (fullWidth != 0) {
				float aspect = this.fullWidth / this.fullHeight;
				float top = (float) System.Math.Tan(Math.degToRad(this.fov * 0.5f))* this.near;
				float bottom = -top;
				float left = aspect * bottom;
				float right = aspect * top;
				float width = System.Math.Abs (right - left);
				float height = System.Math.Abs (top - bottom);

				Matrix4.CreatePerspectiveOffCenter (
					left + this.x * width / this.fullWidth,
				    left + (this.x + this.width) * width / this.fullWidth,
				    top - (this.y + this.height) * height / this.fullHeight,
					top - this.y * height / this.fullHeight,
					this.near,
					this.far,
					out projectionMatrix
				);


			} else {

				Matrix4.CreatePerspectiveFieldOfView(fov,aspect,near,far,out projectionMatrix);
			}

		}




	}
}

