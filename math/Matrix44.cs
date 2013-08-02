using System;

namespace ThreeSharp
{
	public class Matrix44
	{
		public float[] elements;

		public  Matrix44 (float n11=1.0f,float n12=0.0f,float n13=0.0f,float n14=0.0f,
		                float n21=0.0f,float n22=1.0f,float n23=0.0f,float n24=0.0f,
		                float n31=0.0f,float n32=0.0f,float n33=1.0f,float n34=0.0f,
		                float n41=0.0f,float n42=0.0f,float n43=0.0f,float n44=1.0f)
		{
			elements = new float[16];


			elements[0] = n11; elements[4] = n12; elements[8] = n13; elements[12] = n14;
			elements[1] = n21; elements[5] = n22 ; elements[9] = n23; elements[13] = n24 ;
			elements[2] = n31; elements[6] = n32 ; elements[10] = n33; elements[14] = n34;
			elements[3] = n41; elements[7] = n42 ; elements[11] = n43; elements[15] = n44;
		}


		public void set(float n11, float n12, float n13, float n14,
		                float n21, float n22, float n23, float n24,
		                float n31, float n32, float n33, float n34,
		                float n41, float n42, float n43, float n44)
		{
			elements[0] = n11; elements[4] = n12; elements[8] = n13; elements[12] = n14;
			elements[1] = n21; elements[5] = n22 ; elements[9] = n23; elements[13] = n24 ;
			elements[2] = n31; elements[6] = n32 ; elements[10] = n33; elements[14] = n34;
			elements[3] = n41; elements[7] = n42 ; elements[11] = n43; elements[15] = n44;
		}




	}
}

