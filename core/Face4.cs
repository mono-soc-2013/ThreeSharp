using System;
using OpenTK;
using System.Drawing;
using System.Collections;


namespace ThreeSharp
{
	public class Face4
	{
		public float a;
		public float b;
		public float c;
		public float d;
		public Vector3 normal;
		public Color color;
		public ArrayList vertexColors;
		public int materialIndex;
		public Vector3 centroid;

		public Face4 (float a, float b, float c, float d,Vector3 normal=new Vector3(),object color =null,int materialIndex = 0)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;

			this.normal = normal;
			this.color = color is Color? color:new Color();
			this.vertexColors = color is ArrayList? color: new ArrayList();
			this.materialIndex = materialIndex;
			this.centroid = new Vector3();

		}


		public Face4 clone()
		{
			Face4 face = new Face4(a,b,c,d);

			return null;
		}
	}
}

