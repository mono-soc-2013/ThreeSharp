using System;

namespace ThreeSharp
{
	public class CubeGeometry
	{
		float width;
		float height;
		float depth;
		float widthSegment;
		float heightSegment;
		float depthSegment;


		public CubeGeometry (float width,float height,float depth, float widthSegment, float heightSegment,float depthSegment)
		{
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.widthSegment = widthSegment;
			this.heightSegment = heightSegment;
			this.depthSegment = depthSegment;
		}

		public CubeGeometry (float width, float height, float depth)
		{
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.widthSegment = 1.0f;
			this.heightSegment = 1.0f;
			this.depthSegment = 1.0f;

		}


	}
}

