using System;
using System.Collections;
using System.Collections.Generic;

namespace ThreeSharp
{
	public class Scene: Object3D
	{

		public List<Light> __lights;
		public Fog fog;
		public bool autoUpdate;

        public List<Object3D> __objects;
        public List<Object3D> __objectsAdded;
        public List<Object3D> __objectsRemoved; 

        //dynamic parameters in render 
        public List<Mesh> __webglObjects;
        public List<Object> __webglObjectsImmediate;
        public List<Object> __webglSprites;
        public List<Object> __webglFlares;


		public Scene ()
		{
            __lights = new List<Light>();
			fog = null;
            __objects = new List<Object3D>();
            __objectsAdded = new List<Object3D>();
            __objectsRemoved = new List<Object3D>();


			autoUpdate = true; //checked by the renderer 

            //dynamic parameter initialization 
            __webglObjects = new List<Mesh>();
            __webglObjectsImmediate = new List<object>();
            __webglSprites = new List<object>();
            __webglFlares = new List<Object>();

		}


		public void __addObject (Object3D object3d)
		{

			if (object3d is Light) {

                if (this.__lights.IndexOf(object3d as Light) == -1)
                {
                    this.__lights.Add(object3d as Light);
				}

                //if ( object.target && object.target.parent === undefined ) {

                //    this.add( object.target );

                //}
				
            }
            else if (!(object3d is Camera || object3d is Bone))
            {
                if (this.__objects.IndexOf(object3d) == -1)
                {
                    this.__objects.Add(object3d);
                    this.__objectsAdded.Add(object3d);

                    // check if previously removed

                    int i = this.__objectsRemoved.IndexOf(object3d);
                    if (i != -1)
                    {
                        this.__objectsRemoved.RemoveAt(i);
                    }
                }

			}

            for (int i = 0; i < object3d.children.Count; i++)
            {
                this.__addObject(object3d.children[i]);
            }

		}

        public void __removeObject(Object objectt)
        {


		}



	}
}

