using System;
using OpenTK;
using System.Collections.Generic;

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

		CubeGeometry scope;


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

			scope = this;

		}

		public void buildPlane (char u, char v, int udir, int vdir, float width, float height, float depth, int materialindex)
		{
			int ix, iy;
			char w ='c';
			float gridX = scope.widthSegment;
			float gridY = scope.heightSegment;
			width_half = width / 2;
			height_half = height / 2;
			int offSet = scope.vertices.Count;

			if ((u.Equals ('x') && v.Equals ('y')) || (u.Equals ('y') && v.Equals ('x'))) {
				w = 'z';
			} else if ((u.Equals ('x') && v.Equals ('z')) || (u.Equals ('z') && v.Equals ('x'))) {
				w = 'y';
				gridY = scope.depthSegment;
			} else if ((u.Equals ('z') && v.Equals ('y')) || (u.Equals ('y') && v.Equals ('z'))) {
				w = 'x';
				gridY = scope.depthSegment;
			}

			float gridX1 = gridX + 1;
			float gridY1 = gridY + 1;
			float segment_width = width / gridX;
			float segment_height = height / gridY;
			Vector3 normal = new Vector3 ();
			Dictionary<char,float> normaldic = new Dictionary<char, float> ();

			normaldic [w] = depth > 0.0f ? 1.0f : -1.0f;
			normaldic [u] = 0.0f;
			normaldic [v] = 0.0f;

			normal.X = normaldic ['x'];
			normal.Y = normaldic ['y'];
			normal.Z = normaldic ['z'];


			for (iy =0; iy<gridY1; iy++) {

				for (ix = 0; ix< gridX1; ix++) {

					Vector3 vector = new Vector3 ();
					Dictionary<char,float> vectordic = new Dictionary<char, float> ();
					vectordic [u] = (ix * segment_width - width_half) * udir;
					vectordic [v] = (iy * segment_height - height_half) * vdir;
					vectordic [w] = depth;

					vector.X = vectordic ['x'];
					vector.Y = vectordic ['y'];
					vector.Z = vectordic ['z'];

					scope.vertices.Add (vector);

				}
			
			}


			for (iy = 0; iy < gridY; iy++) {

				for(ix = 0; ix < gridX; ix++){

					float a = ix + gridX1 * iy;
					float b = ix + gridX1 * ( iy + 1 );
					float c = ( ix + 1 ) + gridX1 * ( iy + 1 );
					float d = ( ix + 1 ) + gridX1 * iy;


				}
			
			}
		}



	}
}

