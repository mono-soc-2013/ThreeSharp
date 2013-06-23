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

		int attribute_vcol;
		int attribute_vpos;
		int uniform_mview;

		int vbo_position;
		int vbo_color;
		int vbo_mview;

		Vector3[] vertdata;
		Vector3[] coldata;
		Matrix4[] mviewdata;

		public TestWindow ()
		{
		}

		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);

			initProgram();
			vertdata = new Vector3[]{
				new Vector3(-0.8f,-0.8f,0f),
				new Vector3(0.8f,-0.8f,0f),
				new Vector3(0f,0.8f,0f)
			};
			coldata = new Vector3[]{
				new Vector3(1f,0f,0f),
				new Vector3(0f,0f,1f),
				new Vector3(0f,1f,0f)
			};
			mviewdata = new Matrix4[]{Matrix4.Identity};

			Title = "I'm testing openGL with OpenTK";
			GL.ClearColor(Color.CornflowerBlue);
			GL.PointSize(5f);


		}


		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);
			GL.Viewport(0,0,Width,Height);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.Enable(EnableCap.DepthTest);
//			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero,Vector3.UnitZ,Vector3.UnitY);
//			GL.MatrixMode(MatrixMode.Modelview);
//			GL.LoadMatrix(ref modelview);
//
//			GL.Begin(BeginMode.Triangles);
//			GL.Vertex3(-1.0f,-1.0f,4.0f);
//			GL.Vertex3 (1.0f,-1.0f,4.0f);
//			GL.Vertex3(0.0f,1.0f,4.0f);
//			GL.End();



			GL.EnableVertexAttribArray(attribute_vpos);
			GL.EnableVertexAttribArray(attribute_vcol);
			GL.DrawArrays(BeginMode.Triangles,0,3);

			GL.DisableVertexAttribArray(attribute_vpos);
			GL.DisableVertexAttribArray(attribute_vcol);

			GL.Flush();
			SwapBuffers();
		
		
		}



		void initProgram()
		{
			pgmID = GL.CreateProgram();
			loadShader("vs.glsl",ShaderType.VertexShader,pgmID,out vsID);
			loadShader("fs.glsl",ShaderType.FragmentShader,pgmID,out fsID);
			GL.LinkProgram(pgmID);
			Console.WriteLine(GL.GetProgramInfoLog(pgmID));

			attribute_vpos = GL.GetAttribLocation(pgmID,"vPosition"); 
			attribute_vcol = GL.GetAttribLocation(pgmID,"vColor");
			uniform_mview = GL.GetUniformLocation(pgmID,"modelview");
			if(attribute_vpos==-1||attribute_vcol==-1||uniform_mview==-1)
			{
				Console.WriteLine("Error binding attributes");
			}

			GL.GenBuffers(1,out vbo_position);
			GL.GenBuffers(1,out vbo_color);
			GL.GenBuffers(1,out vbo_mview);
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


		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame (e);
			GL.BindBuffer(BufferTarget.ArrayBuffer,vbo_position);
			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(attribute_vpos,3,VertexAttribPointerType.Float,false,0,0);

			GL.BindBuffer(BufferTarget.ArrayBuffer,vbo_color);
			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,(IntPtr)(coldata.Length*Vector3.SizeInBytes),coldata,BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(attribute_vcol,3,VertexAttribPointerType.Float,false,0,0);

			GL.UniformMatrix4(uniform_mview, false, ref mviewdata[0]);

			GL.UseProgram(pgmID);
			GL.BindBuffer(BufferTarget.ArrayBuffer,0);

		}

	}
}

