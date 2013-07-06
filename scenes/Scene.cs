using System;
using System.Collections;

namespace ThreeSharp
{
	public class Scene: Object3D
	{

		public ArrayList __lights;

		public Scene ()
		{
			__lights = new ArrayList();

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

