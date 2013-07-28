using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ThreeSharp
{
    
    class Plane
    {
        //default values
        Vector3 topass = new Vector3(1, 0, 0);
        public Vector3 normal;
        public float constant;

        public Plane(Vector3? normal= null,int constant=0)
        {
            if (normal == null)
                normal = new Vector3(1, 0, 0);


        }

        public void set(Vector3 normal, int constant)
        {
            this.normal = new Vector3(normal);
            this.constant = constant;
        }

        public Plane setComponents(float x, float y, float z, float w)
        {
            this.normal.X = x;
            this.normal.Y = y;
            this.normal.Z = z;

            this.constant = w;
            return this;
        }

        public void normalize()
        {
            float inverseNormalLength = 1.0f / this.normal.Length;
            this.normal.Mult(inverseNormalLength);
            this.constant *= inverseNormalLength;
        }



    }
}
