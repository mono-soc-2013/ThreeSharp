using System;
using OpenTK;
using System.Collections;
using System.Collections.Generic;

namespace ThreeSharp
{
	public class Object3D
	{
		public const string defaultEulerOrder = "XYZ";

		public int id;
		public string name;
		public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
		public List<Object3D> children;
		public Object3D parent;
		public bool matrixAutoUpdate;
		public bool useQuaternion;
		public bool matrixWorldNeedsUpdate;
		public Matrix4 matrix;
		public Matrix4 matrixWorld;
		public string eulerOrder = Object3D.defaultEulerOrder;
        public Quaternion quaternion;

        public bool __webglInit;


		public Object3D ()
		{
            __webglInit = false;
			position = new Vector3();
			children = new List<Object3D>();

            position = new Vector3();
            rotation = new Vector3();
            scale = new Vector3(1, 1, 1);


			parent=null;

			matrixAutoUpdate = true;
            quaternion = new Quaternion();
			useQuaternion = false;
			matrixWorldNeedsUpdate = true;

			matrix = new Matrix4();
			matrixWorld = new Matrix4();
		}


		public void add (Object3D objectt)
		{
			if (objectt.parent != null) {
				objectt.parent.remove(objectt);
			}
			objectt.parent = this;
			this.children.Add(objectt);

			var scene = this;

			while (scene.parent !=null) {
			
				scene = scene.parent;
			}

			if (scene != null && scene is Scene ){

				(scene as Scene).__addObject(objectt);
			}

		}

		public void remove (Object3D objectt)
		{
			int index = this.children.IndexOf (objectt);

			if (index != -1) {

				objectt.parent = null;
				this.children.RemoveAt (index);
			}

			var scene = this;

			while (scene.parent != null) {
			
				scene = scene.parent;
			}

			if (scene != null && scene is Scene) {
				(scene as Scene).__removeObject(objectt);
			}

		}

		public void updateMatrixWorld (bool force= false)
		{
			if (this.matrixAutoUpdate) {
                this.updateMatrix();
			}

			if (matrixWorldNeedsUpdate == true || force == true) {

				if(parent==null)
				{
					matrixWorld = new Matrix4(matrix.Row0,matrix.Row1,matrix.Row2,matrix.Row3);
				}else{

					Matrix4.Mult(parent.matrixWorld,matrix);
				}

				matrixWorldNeedsUpdate = false;
				force = true;
			}

            for (int i = 0; i < this.children.Count; i++)
            {
                this.children[0].updateMatrixWorld(force);
            }

		}


        public void updateMatrix()
		{
            if (!useQuaternion)
            {
                if (eulerOrder == "XYZ")
                {
                    Matrix4.CreateRotationX(rotation.X, out matrix);
                    Matrix4.CreateRotationY(rotation.Y, out matrix);
                    Matrix4.CreateRotationZ(rotation.Z, out matrix);
                }
                else if (eulerOrder == "YXZ")
                {
                    Matrix4.CreateRotationX(rotation.Y, out matrix);
                    Matrix4.CreateRotationY(rotation.X, out matrix);
                    Matrix4.CreateRotationZ(rotation.Z, out matrix);
                }
                else if (eulerOrder == "ZXY")
                {
                    Matrix4.CreateRotationZ(rotation.Z, out matrix);
                    Matrix4.CreateRotationX(rotation.X, out matrix);
                    Matrix4.CreateRotationY(rotation.Y, out matrix);

                }
                else if (eulerOrder == "ZYX")
                {
                    Matrix4.CreateRotationZ(rotation.Z, out matrix);
                    Matrix4.CreateRotationX(rotation.Y, out matrix);
                    Matrix4.CreateRotationY(rotation.X, out matrix);
                }
                else if (eulerOrder == "YZX")
                {
                    Matrix4.CreateRotationX(rotation.Y, out matrix);
                    Matrix4.CreateRotationY(rotation.Z, out matrix);
                    Matrix4.CreateRotationZ(rotation.X, out matrix);
                }
                else if (eulerOrder == "XZY")
                {
                    Matrix4.CreateRotationZ(rotation.X, out matrix);
                    Matrix4.CreateRotationX(rotation.Z, out matrix);
                    Matrix4.CreateRotationY(rotation.Y, out matrix);
                }

                matrix.Row0.Mult(scale.X);
                matrix.Row1.Mult(scale.Y);
                matrix.Row2.Mult(scale.Z);

                //set possiton
                matrix.M41 = position.X;
                matrix.M42 = position.Y;
                matrix.M43 = position.Z;
            }
            else
            {
                //this.matrix.makeFromPositionQuaternionScale( this.position, this.quaternion, this.scale );
                //this will not be executed in the selected sample

            }

            this.matrixWorldNeedsUpdate = true;

		}

		

	}
}

