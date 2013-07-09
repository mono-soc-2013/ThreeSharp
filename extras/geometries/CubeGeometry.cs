using System;
using OpenTK;
using System.Collections.Generic;
using System.Collections;

namespace ThreeSharp
{
	public class CubeGeometry: Geometry
	{
		 int width;
		 int height;
		 int depth;
		 int widthSegment;
		 int heightSegment;
		 int depthSegment;
		 int width_half;
		 int height_half;
		 int depth_half;

		CubeGeometry scope;


		public CubeGeometry (int width,int height,int depth, int widthSegment =1, int heightSegment=1,int depthSegment=1)
		{
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.widthSegment = widthSegment;
			this.heightSegment = heightSegment;
			this.depthSegment = depthSegment;

			int width_half = this.width / 2;
			int height_half = this.height / 2;
			int depth_half = this.depth / 2;

			scope = this;

		}

		public void buildPlane (char u, char v, int udir, int vdir, int width, int height, int depth, int materialindex)
		{
			int ix, iy;
			char w ='c';
			int gridX = scope.widthSegment;
			int gridY = scope.heightSegment;
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

			int gridX1 = gridX + 1;
			int gridY1 = gridY + 1;
			int segment_width = width / gridX;
			int segment_height = height / gridY;
			Vector3 normal = new Vector3 ();
			Dictionary<char,int> normaldic = new Dictionary<char, int> ();

			normaldic [w] = depth > 0 ? 1 : -1;
			normaldic [u] = 0;
			normaldic [v] = 0;

			normal.X = normaldic ['x'];
			normal.Y = normaldic ['y'];
			normal.Z = normaldic ['z'];


			for (iy =0; iy<gridY1; iy++) {

				for (ix = 0; ix< gridX1; ix++) {

					Vector3 vector = new Vector3 ();
					Dictionary<char,int> vectordic = new Dictionary<char, int> ();
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

					int a = ix + gridX1 * iy;
					int b = ix + gridX1 * ( iy + 1 );
					int c = ( ix + 1 ) + gridX1 * ( iy + 1 );
					int d = ( ix + 1 ) + gridX1 * iy;

					Face4 face = new Face4(a+offSet,b+offSet,c+offSet,d+offSet);
					face.normal = new Vector3(normal);
					face.vertexNormals.Add(new Vector3(normal));
					face.vertexNormals.Add(new Vector3(normal));
					face.vertexNormals.Add(new Vector3(normal));
					face.vertexNormals.Add(new Vector3(normal));
					face.materialIndex = materialindex;

					scope.faces.Add(face);

					scope.faceVertexUvs.Add(new ArrayList());
					((ArrayList)scope.faceVertexUvs[0]).Add(new Vector2(ix / gridX, 1 - iy / gridY ));
					((ArrayList)scope.faceVertexUvs[0]).Add(new Vector2( ix / gridX, 1 - ( iy + 1 ) / gridY ));
					((ArrayList)scope.faceVertexUvs[0]).Add(new Vector2( ( ix + 1 ) / gridX, 1- ( iy + 1 ) / gridY ));
					((ArrayList)scope.faceVertexUvs[0]).Add(new Vector2( ( ix + 1 ) / gridX, 1 - iy / gridY ));

				}
			
			}

			this.computeCentroids();
			this.mergeVertices();
		}



	}
}

