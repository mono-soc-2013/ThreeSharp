using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.IO;

namespace TestThreeSharp
{
	public class TestWindow : GameWindow
	{
		int pgmID;
		int vsID;
		int fsID;

		public TestWindow ()
		{
		}

		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			Title = "I'm testing openGL with OpenTK";
			GL.ClearColor(Color.CornflowerBlue);


		}


		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero,Vector3.UnitZ,Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			GL.Begin(BeginMode.Triangles);
			GL.Vertex3(-1.0f,-1.0f,4.0f);
			GL.Vertex3 (1.0f,-1.0f,4.0f);
			GL.Vertex3(0.0f,1.0f,4.0f);
			GL.End();


			SwapBuffers();
		
		
		}

		protected override void OnResize (EventArgs e)
		{
			base.OnResize (e);
			GL.Viewport(ClientRectangle.X,ClientRectangle.Y,ClientRectangle.Width,ClientRectangle.Height);
			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI/4,Width/(float)Height,1.0f,64.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
		
		}


		void initProgram()
		{

			pgmID = GL.CreateProgram();
		}

		void loadShader(String filename,ShaderType type, int program, out int address)
		{
			address = GL.CreateShader(type);
			using(StreamReader reader = new StreamReader(filename))
			{
				GL.ShaderSource(address,reader.ReadToEnd());
			}
			GL.CompileShader(address);
			GL.AttachShader(program,address);
			Console.WriteLine(GL.GetShaderInfoLog(address));
		}

	}
}

