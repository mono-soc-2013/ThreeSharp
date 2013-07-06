using System;

namespace ThreeSharp
{
	public class CubeGeometry: Geometry
	{
		float width;
		float height;
		float depth;
		float widthSegment;
		float heightSegment;
		float depthSegment;

		float width_half;
		float height_half;
		float depth_half;


		public CubeGeometry (float width,float height,float depth, float widthSegment =1.0f, float heightSegment=1.0f,float depthSegment=1.0f)
		{
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.widthSegment = widthSegment;
			this.heightSegment = heightSegment;
			this.depthSegment = depthSegment;

			float width_half = this.width / 2;
			float height_half = this.height / 2;
			float depth_half = this.depth / 2;


		}



	}
}

