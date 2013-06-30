using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Input;
 
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
 
namespace TestThreeSharp
{
 
    class Game : GameWindow
    {
        private Matrix4 modelviewMatrix, projectionMatrix;
        private Matrix4 rotationviewMatrix;
 
        int fragmentShaderHandle, shaderProgramHandle, texture0, texture1, texture2, texture3;
 
        const float rotation_speed = 18.0f;
 
        float angle;
 
        Bitmap bitmap0 = new Bitmap(@"pic1.jpg");
        Bitmap bitmap1 = new Bitmap(@"pic2.jpg");
        Bitmap bitmap2 = new Bitmap(@"pic3.jpg");
        Bitmap bitmap3 = new Bitmap(@"pic4.jpg");
 
        string fragmentShaderSource = @"
 
			#version 150
			uniform sampler2D MyTexture0;
			uniform sampler2D MyTexture1;
			uniform sampler2D MyTexture2;
			uniform sampler2D MyTexture3;
			 
			void main(void)
			{
			 if (gl_TexCoord[0].s < 0.25){
			  gl_FragColor = texture2D( MyTexture0, gl_TexCoord[0].st );  
			  gl_FragColor[1] = gl_FragColor[1] * 0.90;
			 }
			else if (gl_TexCoord[0].s < 0.5) {
			  gl_FragColor = texture2D( MyTexture1, gl_TexCoord[0].st );  
			  gl_FragColor[0] = gl_FragColor[0] * 0.90;
			 }
			else if (gl_TexCoord[0].s < 0.75) {
			  gl_FragColor = texture2D( MyTexture2, gl_TexCoord[0].st );  
			  gl_FragColor[2] = gl_FragColor[2] * 0.90;
			 }
			else {
			  gl_FragColor = texture2D( MyTexture3, gl_TexCoord[0].st );  
			 }
			}"
            ;
 
        void CreateShaders()
        {
 
            fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderHandle, fragmentShaderSource);
            GL.CompileShader(fragmentShaderHandle);
            Debug.WriteLine(GL.GetShaderInfoLog(fragmentShaderHandle));
 
            // Create program
            shaderProgramHandle = GL.CreateProgram();
            GL.AttachShader(shaderProgramHandle, fragmentShaderHandle);
            GL.LinkProgram(shaderProgramHandle);
            Debug.WriteLine(GL.GetProgramInfoLog(shaderProgramHandle));
 
            GL.UseProgram(shaderProgramHandle);
 
 
            float aspectRatio = ClientSize.Width / (float)(ClientSize.Height);
            Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, aspectRatio, 1, 100, out projectionMatrix);
        }
 
        /// <summary>Creates a 800x600 window with the specified title.</summary>
        public Game(): base(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample")
        {
            VSync = VSyncMode.On;
        }
 
        /// <summary>Load resources here.</summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            CreateShaders();
            base.OnLoad(e);
 
            GL.ClearColor(0.0f, 0.4f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
 
            CreateTexture(out texture0, bitmap0);
            CreateTexture(out texture1, bitmap1);
            CreateTexture(out texture2, bitmap2);
            CreateTexture(out texture3, bitmap3);
 
            BindTexture(ref texture0, TextureUnit.Texture0, "MyTexture0");
            BindTexture(ref texture1, TextureUnit.Texture1, "MyTexture1");
            BindTexture(ref texture2, TextureUnit.Texture2, "MyTexture2");
            BindTexture(ref texture3, TextureUnit.Texture3, "MyTexture3");
 
            modelviewMatrix = Matrix4.LookAt(0, 4, 7, 0, 0, 0, 0, 1, 0);
 
            rotationviewMatrix = Matrix4.CreateRotationX((float)Math.PI / 100);// *Matrix4.CreateRotationX((float)Math.PI / 10000);
        }
 
        private void CreateTexture(out int texture, Bitmap bitmap)
        {
            // load texture 
            GL.GenTextures(1, out texture);
 			
            //Still required else TexImage2D will be applyed on the last bound texture
            GL.BindTexture(TextureTarget.Texture2D, texture);
 
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
 
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
 
            bitmap.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        }
 
        /// <summary>
        /// Called when your window is resized. Set your viewport here. It is also
        /// a good place to set up your projection matrix (which probably changes
        /// along when the aspect ratio of your window).
        /// </summary>
        /// <param name="e">Not used.</param>
 
        protected override void OnResize(EventArgs e)
        {
 
            base.OnResize(e);
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);
 
        }
 
        /// <summary>
        /// Called when it is time to setup the next frame. Add you game logic here.
        /// </summary>
        /// <param name="e">Contains timing information for framerate independent logic.</param>
 
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (Keyboard[Key.Escape])
                Exit();
        }
 
        /// <summary>
        /// Called when it is time to render the next frame. Add your rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
 
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            angle += rotation_speed * (float)e.Time;
            float translateby = (float)Math.Sin(DateTime.Now.Second) * 0.05f;
            rotationviewMatrix = Matrix4.CreateTranslation(0f, 0f, translateby);
 
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelviewMatrix);
            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);
 
 
            DrawCube();
            SwapBuffers();
        }
 
        private void BindTexture(ref int textureId, TextureUnit textureUnit, string UniformName)
        {
            GL.ActiveTexture(textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.Uniform1(GL.GetUniformLocation(shaderProgramHandle, UniformName), textureUnit - TextureUnit.Texture0);
        }
 
 
 
        private void DrawCube()
        {
            GL.Begin(BeginMode.Quads);
 
            GL.Color3(Color.Silver);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
 
            GL.Color3(Color.Honeydew);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
 
            GL.Color3(Color.Goldenrod);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
 
 
            GL.Color3(Color.DodgerBlue);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
 
            GL.Color3(Color.Purple);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
 
            GL.Color3(Color.ForestGreen);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
 
            GL.End();
 
        }
 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
 
//        [STAThread]
//        static void Main()
//        {
//            // The 'using' idiom guarantees proper resource cleanup.
//            // We request 30 UpdateFrame events per second, and unlimited
//            // RenderFrame events (as fast as the computer can handle).
//            using (Game game = new Game())
//            {
//                game.Run(30.0);
//            }
//        }
    }
}
