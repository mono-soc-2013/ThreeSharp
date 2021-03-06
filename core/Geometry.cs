using System;
using OpenTK;
using System.Collections;
using System.Collections.Generic;

namespace ThreeSharp
{
   

    public struct GeoGroupStruct{
        public int id;
        public bool __webglVertexBuffer;
        public List<Face3> faces3;
        public List<Face4> faces4;
        public int materialIndex;
        public int vertices;
        public int numMorphTargets;
        public int numMorphNormals;

        public GeoGroupStruct(List<Face3> faces3, List<Face4> faces4, int materialIndex, int vertices, int numMorphTargets, int numMorphNormals)
        {
            this.faces3 = faces3;
            this.faces4 = faces4;
            this.materialIndex = materialIndex;
            this.vertices = vertices;
            this.numMorphTargets = numMorphTargets;
            this.numMorphNormals = numMorphNormals;
            this.id = 0;
            this.__webglVertexBuffer = false;
        }
    }

	public class Geometry
	{
		public List<Vector3> vertices; 
		public ArrayList faces;
		public List<List<List<Vector2>>> faceVertexUvs;
		public ArrayList __tmpVertices;
        public Sphere boundingSphere = null;

        public List<Object> morphTargets = null;
        public List<Object> morphColors = null;
        public List<Object> morphNormals = null;

        public bool __webglInit;

        public Dictionary<string,GeoGroupStruct> geometryGroups = null;
        public List<GeoGroupStruct> geometryGroupsList = null;

        public bool _normalMatrix;

		public Geometry ()
		{
			vertices = new List<Vector3>();

            morphTargets = new List<object>();
            morphColors = new List<object>();
            morphNormals = new List<object>();

			faces = new ArrayList();
			faceVertexUvs = new List<List<List<Vector2>>>();
            _normalMatrix = false;
            __webglInit = false;
		}

		public void computeCentroids ()
		{
			int f, fl;
			Face4 face4;
			Face3 face3;

			for (f=0,fl= this.faces.Count; f<fl; f++) {

				if(faces[f] is Face3){

					face3 = (Face3)this.faces[f];
					face3.centroid.X = 0.0f;face3.centroid.Y = 0.0f; face3.centroid.Z = 0.0f;
			
					face3.centroid.Add(vertices[ face3.a ] );
					face3.centroid.Add(this.vertices[ face3.b ] );
					face3.centroid.Add(this.vertices[ face3.c ] );	

					face3.centroid.Div(3.0f);

				}else if(faces[f] is Face4){
					face4 = (Face4)this.faces[f];
					face4.centroid.X = 0.0f;face4.centroid.Y = 0.0f; face4.centroid.Z = 0.0f;

					face4.centroid.Add(this.vertices[ face4.a] );
					face4.centroid.Add(this.vertices[ face4.b] );
					face4.centroid.Add(this.vertices[ face4.c] );	
					face4.centroid.Add(this.vertices[ face4.d] );	

					face4.centroid.Div(4.0f);
				}


			}
		}

		public int mergeVertices ()
		{
			Dictionary<string,int> verticesMap = new Dictionary<string,int> ();
			List<Vector3> unique = new List<Vector3> ();
			Dictionary<int,int> changes = new Dictionary<int, int> ();
			Vector3 v;
			String key;
			int precisionPoints = 4;// number of decimal points, eg. 4 for epsilon of 0.0001
			double precision = System.Math.Pow (10, precisionPoints);
			int i, il, j, jl;
			object face;
			List<Vector2> u;
			List<int> indices = new List<int>();
			// reset cache of vertices as it now will be changing.
			this.__tmpVertices = null;

			for (i=0,il = this.vertices.Count; i<il; i++) {

				v = this.vertices [i];
				int[] key_array = {
					(int)System.Math.Round (v.X * precision),
					(int)System.Math.Round (v.Y * precision),
					(int)System.Math.Round (v.Z * precision)
				};
				key = String.Join ("-", key_array);

				if (!verticesMap.ContainsKey (key)) {

					verticesMap [key] = i;
					unique.Add (this.vertices [i]);
					changes [i] = unique.Count - 1;
				} else {
					//console.log('Duplicate vertex found. ', i, ' could be using ', verticesMap[key]);
					changes [i] = changes [verticesMap [key]];
				}
			}

			// if faces are completely degenerate after merging vertices, we
			// have to remove them from the geometry.

			List<int> faceIndicesToRemove = new List<int> ();

			for (i=0,il= faces.Count; i<il; i++) {
				Face3 face3;
				Face4 face4;

				if (faces [i] is Face3) {
					face = face3 = (Face3)faces [i];
					face3.a = changes [face3.a];
					face3.b = changes [face3.b];
					face3.c = changes [face3.c];


					indices.Add (face3.a);
					indices.Add (face3.b);
					indices.Add (face3.c);

					int dupIndex = -1;
					// if any duplicate vertices are found in a Face3
					// we have to remove the face as nothing can be saved
					for (int n = 0; n < 3; n ++) {
						if (indices [n] == indices [(n + 1) % 3]) {

							dupIndex = n;
							faceIndicesToRemove.Add (i);
							break;

						}
					}

				} else if (faces [i] is Face4) {

					face = face4 = (Face4)faces [i];

					face4.a = changes [face4.a];
					face4.b = changes [face4.b];
					face4.c = changes [face4.c];
					face4.d = changes [face4.d];

					// check dups in (a, b, c, d) and convert to -> face3
					indices.Add (face4.a);
					indices.Add (face4.b);
					indices.Add (face4.c);
					indices.Add (face4.d);

					int dupIndex = -1;
					for (var n = 0; n < 4; n ++) {

						if (indices [n] == indices [(n + 1) % 4]) {

							// if more than one duplicated vertex is found
							// we can't generate any valid Face3's, thus
							// we need to remove this face complete.
							if (dupIndex >= 0) {

								faceIndicesToRemove.Add (i);

							}

							dupIndex = n;

						}
					}

					if (dupIndex >= 0) {
						indices.RemoveAt (dupIndex);
						Face3 newFace = new Face3 ((int)indices [0], (int)indices [1], (int)indices [2], face is Face3 ? ((Face3)face).normal : ((Face4)face).normal, face is Face3 ? ((Face3)face).color : ((Face4)face).color, face is Face3 ? ((Face3)face).materialIndex : ((Face4)face).materialIndex);

						for (j=0,jl = this.faceVertexUvs.Count; j < jl; j++) {

							u = faceVertexUvs [j][i];
							if (u!=null) {
								u.RemoveAt (dupIndex);
							}
						}

						if ((face is Face3 ? ((Face3)face).vertexNormals : ((Face4)face).vertexNormals).Count > 0) {

							newFace.vertexNormals = face is Face3 ? ((Face3)face).vertexNormals : ((Face4)face).vertexNormals;
							newFace.vertexNormals.RemoveAt (dupIndex);
						}

						if ((face is Face3 ? ((Face3)face).vertexColors : ((Face4)face).vertexColors).Count > 0) {

							newFace.vertexColors = face is Face3 ? ((Face3)face).vertexColors : ((Face4)face).vertexColors;
							newFace.vertexColors.RemoveAt (dupIndex);
						}

						this.faces [i] = newFace;

					}


				}

			}

			for (i = faceIndicesToRemove.Count -1; i>=0; i--) 
			{

				this.faces.RemoveAt(i);

				for(j=0, jl = this.faceVertexUvs.Count; j<jl;j++)
				{
					this.faceVertexUvs[j].RemoveAt(j);
				}
			
			}

			// Use unique set of vertices

			int diff = this.vertices.Count - unique.Count;
			this.vertices = unique;
			return diff;
		}


        public void computeBoundingSphere()
        {
            if (this.boundingSphere == null)
            {
                this.boundingSphere = new Sphere();
            }

            this.boundingSphere.setFromCenterAndPoints(this.boundingSphere.center, this.vertices);
        }
	}
}

