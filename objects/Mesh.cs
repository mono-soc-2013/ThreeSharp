using System;
using OpenTK;
namespace ThreeSharp
{
	public class Mesh: Object3D
	{
		public Geometry geometry = null;
		public Material material = null;

        
        public Matrix4 _modelViewMatrix;
        public Matrix4 _normalMatrix;




		public Mesh (Geometry geometry,Material material)
		{

            this.setGeometry(geometry);
            this.setMaterial(material);
		}


        public void setGeometry(Geometry geometry)
        {
            this.geometry = geometry;
            if (this.geometry.boundingSphere == null)
            {
                this.geometry.computeBoundingSphere();
            }

            updateMorphTargets();
        }


        public void setMaterial(Material material=null)
        {
            if (material != null)
            {
                this.material = material;
            }
            else
            {
                this.material = new MeshBasicMaterial();
            }
        }


        public void updateMorphTargets()
        {

        }
	}
}

