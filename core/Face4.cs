using System;
using OpenTK;
using System.Drawing;
using System.Collections;


namespace ThreeSharp
{
	public class Face4
	{
		public int a;
		public int b;
		public int c;
		public int d;
		public Vector3 normal;
		public ArrayList vertexNormals;
		public Color color;
		public ArrayList vertexColors;
		public int materialIndex;
		public Vector3 centroid;
		public ArrayList vertexTangents;


		public Face4 (int a, int b, int c, int d,object normal=null,object color =null,int materialIndex = 0)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;

			this.normal = normal is Vector3? (Vector3)normal: new Vector3();
			this.vertexNormals = normal is ArrayList? (ArrayList)normal: new ArrayList();
			this.color = color is Color? (Color)color:new Color();
			this.vertexColors = color is ArrayList? (ArrayList)color: new ArrayList();
			this.materialIndex = materialIndex;
			this.centroid = new Vector3();
			this.vertexTangents = new ArrayList();

		}


		public Face4 clone()
		{
			Face4 face = new Face4(a,b,c,d);
			face.normal = new Vector3(this.normal);
			face.color = Color.FromArgb(this.color.ToArgb());
			face.centroid = new Vector3(this.centroid);

			int i,il;
			for ( i = 0, il = this.vertexNormals.Count; i < il; i ++ ) face.vertexNormals[ i ] = new Vector3((Vector3)this.vertexNormals[ i ]);
			for ( i = 0, il = this.vertexColors.Count; i < il; i ++ ) face.vertexColors[ i ] = Color.FromArgb(((Color)this.vertexColors[ i ]).ToArgb());
			for ( i = 0, il = this.vertexTangents.Count; i < il; i ++ ) face.vertexTangents[ i ] = new Vector3((Vector3)(this.vertexTangents[ i ]));

			return face;
		}
	}
}

