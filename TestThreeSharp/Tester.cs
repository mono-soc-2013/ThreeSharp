using System;
using ThreeSharp;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Drawing;


namespace TestThreeSharp
{
	public class Tester
	{
		enum VAO_IDs {triangles, NumVAOs};
		static uint[] VAOs;

		public Tester ()
		{

		}

//		public static void Main (String[] args)
//		{
//			/*OpenGLRenderer renderr = new OpenGLRenderer();
//			renderr.setSize(600,700);
//
//			PerspectiveCamera camera = new PerspectiveCamera(70.0f,800.0f/600.0f,1.0f,1000.0f);
//			camera.Z_POSSITION = 400.0f;*/
//
//
////			GLControl control = new GLControl(GraphicsMode.Default,1,0,GraphicsContextFlags.Default);
////			control.MakeCurrent();
////
////			VAOs = new uint[(int)VAO_IDs.NumVAOs];
//
//			//GL.GenVertexArrays((int)VAO_IDs.NumVAOs,VAOs);
////			init();
//
//
//		
////			control.SwapBuffers();
//
//			using(TestWindow win = new TestWindow())
//			{
//				win.Run (30.0);
//			}
//
//
//		}

		public static void init ()
		{
			GL.GenVertexArrays((int)VAO_IDs.NumVAOs,VAOs);
			GL.BindVertexArray(VAOs[(int)VAO_IDs.triangles]);


		}




	}
}

