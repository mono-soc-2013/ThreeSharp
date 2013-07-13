using System;
using OpenTK;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;


namespace ThreeSharp
{
	public class Face3
	{
		public int a;
		public int b;
		public int c;
		public Vector3 normal;
		public List<Vector3> vertexNormals;
		public Color color;
		public List<Color> vertexColors;
		public int materialIndex;
		public Vector3 centroid;
		public List<Vector3> vertexTangents;

		public Face3 (int a, int b, int c,object normal=null,object color =null,int materialIndex = 0)
		{
			this.a = a;
			this.b = b;
			this.c = c;

			this.normal = normal is Vector3? (Vector3)normal: new Vector3();
			this.vertexNormals = normal is List<Vector3>? (List<Vector3>)normal: new List<Vector3>();
			this.color = color is Color? (Color)color:new Color();
			this.vertexColors = color is List<Color>? (List<Color>)color: new List<Color>();
			this.materialIndex = materialIndex;
			this.centroid = new Vector3();
			this.vertexTangents = new List<Vector3>();
		}

		public Face3 clone()
		{
			Face3 face = new Face3(a,b,c);
			face.normal = new Vector3(this.normal);
			face.color = Color.FromArgb(this.color.ToArgb());
			face.centroid = new Vector3(this.centroid);

			int i,il;
			for ( i = 0, il = this.vertexNormals.Count; i < il; i ++ ) face.vertexNormals.Add(new Vector3(this.vertexNormals[ i ]));
			for ( i = 0, il = this.vertexColors.Count; i < il; i ++ ) face.vertexColors.Add(Color.FromArgb(vertexColors[ i ].ToArgb()));
			for ( i = 0, il = this.vertexTangents.Count; i < il; i ++ ) face.vertexTangents.Add(new Vector3(this.vertexTangents[ i ]));

			return face;
		}
	}
}

