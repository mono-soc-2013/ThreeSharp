using System;

namespace ThreeSharp
{
	public static class Math
	{


		public static double degToRad(float degrees){
		
			float degreeToRadiansFactor = (float)System.Math.PI / 180;

			return Convert.ToDouble(degreeToRadiansFactor*degrees);

		}

		public static double radToDeg(double rad){
			double radianToDegreesFactor =  (180.0f / System.Math.PI);

			return radianToDegreesFactor*rad;
		}



	}
}

