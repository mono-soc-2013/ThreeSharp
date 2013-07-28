using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ThreeSharp
{
    class Frustum
    {

        public Plane[] planes;

        public Frustum(Plane p0,Plane p1,Plane p2,Plane p3,Plane p4,Plane p5)
        {

            planes = new Plane[] { p0 != null ? p0 : new Plane(),
                                    p1!=null?p1:new Plane(),
                                    p2!=null?p2:new Plane(),
                                    p3!=null?p3:new Plane(),
                                    p4!=null?p4:new Plane(),
                                    p5!=null?p5:new Plane()};


        }


        public void setFromMatrix(Matrix4 m)
        {
            planes[0].setComponents(m.M14 - m.M11, m.M24 - m.M21, m.M34 - m.M31, m.M44 - m.M41).normalize();
            planes[1].setComponents(m.M14 + m.M11, m.M24 + m.M21, m.M34 + m.M31, m.M44 + m.M41).normalize();
            planes[2].setComponents(m.M14 + m.M12, m.M24 + m.M22, m.M34 + m.M32, m.M44 + m.M42).normalize();
            planes[3].setComponents(m.M14 - m.M12, m.M24 - m.M22, m.M34 - m.M32, m.M44 - m.M42).normalize();
            planes[4].setComponents(m.M14 - m.M13, m.M24 - m.M23, m.M34 - m.M33, m.M44 - m.M43).normalize();
            planes[5].setComponents(m.M14 + m.M13, m.M24 + m.M23, m.M34 + m.M33, m.M44 + m.M43).normalize();
        }
    }
}
