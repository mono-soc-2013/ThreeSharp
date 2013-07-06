using System;
using OpenTK;
using System.Collections;

namespace ThreeSharp
{
	public class Object3D
	{
		public int id;
		public string name;
		public Vector3 position;
		public ArrayList children;
		public Object3D parent;


		public Object3D ()
		{
			position = new Vector3();
			children = new ArrayList();

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
	}
}

