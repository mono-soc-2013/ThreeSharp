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

        //dynamic parameters in render 
        public 

		public Scene ()
		{
            __lights = new List<Light>();
			fog = null;

			autoUpdate = true; //checked by the renderer 

		}


		public void __addObject (Object objectt)
		{

			if (objectt is Light) {

				if(this.__lights.IndexOf(objectt as Light) == -1){
					this.__lights.Add(objectt as Light);
				}

//				if ( object.target && object.target.parent === undefined ) {
//
//					this.add( object.target );
//
//				}
			}else if(!(objectt is Camera || objectt is Bone)){
			

			}

		}

		public void __removeObject(Object objectt){


		}



	}
}

