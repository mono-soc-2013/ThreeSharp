using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ThreeSharp
{
    public class Sphere
    {
        public Vector3 center;
        public double radius;

        public Sphere(Vector3? center = null, int radius = 0)
        {
            if (center == null)
            {
                this.center = new Vector3();
            }
            else
            {
                this.center = (Vector3)center;
            }

            this.radius = radius;
        }


        public void setFromCenterAndPoints(Vector3 center, List<Vector3> points)
        {
            float maxRadiusSq = 0;

            for (int i = 0; i < points.Count; i++)
            {

                float radiusSq = (center - points[i]).LengthSquared;
                maxRadiusSq = System.Math.Max(maxRadiusSq, radiusSq);

            }

            this.center = center;
            this.radius = (double)System.Math.Sqrt((double)maxRadiusSq);
        }

    }
}
