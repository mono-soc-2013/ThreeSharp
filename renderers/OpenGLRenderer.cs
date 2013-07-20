using System;
using OpenTK;
using OpenTK.Graphics.ES20;
using OpenTK.Input;
using OpenTK.Graphics;
using System.Drawing;
using System.Collections.Generic;

namespace ThreeSharp
{
	public class OpenGLRenderer: GameWindow
	{

		public Color4 _clearColor;
		public float _clearAlpha;
	 

		//public properties
		public float devicePixelRatio;


		//clearing 
		public bool autoClear = true;
		public bool autoClearColor = true;
		public bool autoClearDepth = true;
		public bool autoClearStencil = true;

		// scene graph

		public bool sortObjects = true;
		public bool autoUpdateObjects = true;

		// physically based shading

		public bool gammaInput = false;
		public bool gammaOutput = false;
		public bool physicallyBasedShading = false;

		// shadow map

		public bool shadowMapEnabled = false;
		public bool shadowMapAutoUpdate = true;
		public int shadowMapType = Three.PCFShadowMap;
		public int shadowMapCullFace = Three.CullFaceFront;
		public bool shadowMapDebug = false;
		public bool shadowMapCascade = false;

		// morphs

		public int maxMorphTargets = 8;
		public int maxMorphNormals = 4;

		// flags

		public bool autoScaleCubemaps = true;

		// custom render plugins

		public object renderPluginsPre = null;
		public object renderPluginsPost = null;


		///////////////////////////////////
		public bool _glExtensionTextureFloat = false;
		public bool _glExtensionStandardDerivatives =false;
		public bool _glExtensionTextureFilterAnisotropic =false;
		public bool _glExtensionCompressedTextureS3TC =false;

		//GPU Capabilities



		public int _maxAnisotropy = 0;

		public OpenGLRenderer (Color? clearColor =null ,float clearAlpha=1.0f, float devicePixelRatio=1.0f)
		{


			_clearColor = new Color4(clearColor.HasValue?clearColor.Value:(Color)(new ColorConverter()).ConvertFromString("#000000"));
			_clearAlpha = clearAlpha;

			devicePixelRatio = devicePixelRatio;

		}

	    protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			initGL();
			setDefaultGLState();



			//_maxAnisotropy = _glExtensionStandardDerivatives? GL.GetTexParameter(Textur
		}

		public void initGL ()
		{
			//string[] extensions = (GL.GetString (StringName.Extensions)).Split (' ');
			
			List<string> extensions = new List<string>((GL.GetString (StringName.Extensions)).Split (' '));
			if(extensions.Contains("OES_texture_float"))
				_glExtensionTextureFloat = true;

			if(extensions.Contains("OES_standard_derivatives"))
				_glExtensionStandardDerivatives = true;

			if(extensions.Contains("EXT_texture_filter_anisotropic")||
			   extensions.Contains("MOZ_EXT_texture_filter_anisotropic")||
			   extensions.Contains("WEBKIT_EXT_texture_filter_anisotropic"))
				_glExtensionTextureFilterAnisotropic = true;

			if(extensions.Contains("WEBGL_compressed_texture_s3tc")||
			   extensions.Contains("MOZ_WEBGL_compressed_texture_s3tc")||
			   extensions.Contains("WEBKIT_WEBGL_compressed_texture_s3tc"))
				_glExtensionCompressedTextureS3TC = true;


		
		}


		public void setDefaultGLState()
		{
			GL.ClearColor(0.0f,0.0f,0.0f,1.0f);
			GL.ClearDepth(1.0f);
			GL.ClearStencil(0);

			GL.Enable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Lequal);

			GL.FrontFace(FrontFaceDirection.Ccw);
			GL.CullFace(CullFaceMode.Back);
			GL.Enable(EnableCap.CullFace);

			GL.Enable(EnableCap.Blend);
			GL.BlendEquation(BlendEquationMode.FuncAdd);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha,BlendingFactorDest.OneMinusSrcAlpha);

			GL.ClearColor(_clearColor.R,_clearColor.G,_clearColor.B,_clearAlpha);


		}


		public void setSize(int width, int height)
		{
			this.Size = new Size(width,height);
		}


		public int getMaxAnisotropy()
		{

			return _maxAnisotropy;
		}


		public void render ()
		{


		}
	}
}

