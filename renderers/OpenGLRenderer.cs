using System;
using OpenTK;
using OpenTK.Graphics.ES20;
using OpenTK.Input;
using OpenTK.Graphics;
using System.Drawing;
using System.Collections.Generic;

namespace ThreeSharp
{
    public struct HashMapStruct
    {
        public int hash;
        public int counter;

        public HashMapStruct(int hash, int counter)
        {
            this.hash = hash;
            this.counter = counter;
        }
    }

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


		//internal state cache
		public int _currentMaterialId = -1;

		// light arrays cache
		public bool _lightsNeedUpdate = true;


        // camera matrices cache
        public Matrix4 _projScreenMatrix = new Matrix4();
        public Matrix4 _projScreenMatrixPS = new Matrix4();


        //// frustum

        public Frustum _frustum = new Frustum(null,null,null,null,null,null);


        // internal state cache
        int _geometryGroupCounter = 0;

		public OpenGLRenderer (Color? clearColor =null ,float clearAlpha=1.0f, float devicePixelRatio=1.0f)
		{


			_clearColor = new Color4(clearColor.HasValue?clearColor.Value:(Color)(new ColorConverter()).ConvertFromString("#000000"));
			_clearAlpha = clearAlpha;

			this.devicePixelRatio = devicePixelRatio;

		}

	    protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			initGL();
			setDefaultGLState();

			GL.GetBufferParameter(BufferTarget.ArrayBuffer,BufferParameterName.BufferSize,out _maxAnisotropy);

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


		public void render (Scene scene, Camera camera)
		{
			List<Light> lights = scene.__lights;
			Fog fog = scene.fog;

			//reset caching for this frame
			_currentMaterialId = -1;
			_lightsNeedUpdate = true;


			//update scene graph
            if (scene.autoUpdate)
                scene.updateMatrixWorld();

            // update camera matrices and frustum

            if (camera.parent == null)
                camera.updateMatrixWorld();

            camera.matrixWorldInverse.Invert();

            Matrix4.Mult(ref camera.projectionMatrix, ref camera.matrixWorldInverse, out _projScreenMatrix);

            _frustum.setFromMatrix(_projScreenMatrix);

            // update WebGL objects

            //if(this.autoUpdateObjects)

            
		}


        public void initWebGLObjects(Scene scene)
        {
            //Scene initialization 


            //Object added length >> while


            //Object removed length >> while

            while (scene.__objectsAdded.Count > 0)
            {

            }


            // update must be called after objects adding / removal
            for (int o = 0; o < scene.__webglObjects.Count; o++)
            {
                addObject(scene.__objectsAdded[0],scene);
            }
        }


        public void addObject(Object3D object3d, Scene scene)
        {

            Geometry geometry;
            Material material;

            if (!object3d.__webglInit)
            {
                object3d.__webglInit = true;
                Mesh mesh = (Mesh)object3d;
                mesh._modelViewMatrix = new Matrix4();
                mesh._normalMatrix = new Matrix4();
                GeoGroupStruct geometryGroup;

                if (mesh.geometry != null && !mesh.geometry.__webglInit)
                {
                    mesh.geometry.__webglInit = true;

                    //Add event listner to geometty 
                    //Not implemented
                }

                geometry = mesh.geometry;

                if (geometry == null)
                {
                    // fail silently for now
                }
                else if(object3d is Mesh)
                {
                    material = mesh.material;

                    if (geometry.geometryGroups == null)
                    {
                        sortFacesByMaterial(geometry, material);
                    }

                    // create separate VBOs per geometry chunk
                    foreach (string key in geometry.geometryGroups.Keys)
                    {
                        geometryGroup = geometry.geometryGroups[key];
                        // initialise VBO on the first access

                        if (!geometryGroup.__webglVertexBuffer)
                        {

                        }
                    }

                }

            }

        }


        // Geometry splitting
        public void sortFacesByMaterial(Geometry geometry, Material material)
        {
            Object face;
            int materialIndex, vertices;
            string groupHash;
            Dictionary<int, HashMapStruct> hash_map = new Dictionary<int, HashMapStruct>();

            int numMorphTargets = geometry.morphTargets.Count;
            int numMorphNormals = geometry.morphNormals.Count;

            bool usesFaceMaterial = material.GetType() == typeof(MeshBasicMaterial);

            //Check this code later 
            geometry.geometryGroups = new Dictionary<string, GeoGroupStruct>();

            for (int f = 0; f < geometry.faces.Count; f++)
            {
               
                
                face = geometry.faces[f];
                materialIndex = usesFaceMaterial? (face is Face3?((Face3)face).materialIndex:((Face4)face).materialIndex):0;


                if (!hash_map.ContainsKey(materialIndex))
                {
                    hash_map[materialIndex] = new HashMapStruct(materialIndex, 0);
                }

                groupHash = hash_map[materialIndex].hash.ToString() + "_" + hash_map[materialIndex].counter;


                if (!geometry.geometryGroups.ContainsKey(groupHash))
                {
                    geometry.geometryGroups[groupHash] = new GeoGroupStruct(new List<Face3>(), new List<Face4>(), materialIndex, 0, numMorphTargets, numMorphNormals);

                }

                vertices = face is Face3 ? 3 : 4;

                if (geometry.geometryGroups[groupHash].vertices + vertices > 65535)
                {
                    hash_map[materialIndex] = new HashMapStruct(hash_map[materialIndex].hash, hash_map[materialIndex].counter + 1);
                    groupHash = hash_map[materialIndex].hash.ToString() + '_' + hash_map[materialIndex].counter.ToString();

                    if (!geometry.geometryGroups.ContainsKey(groupHash))
                    {
                        geometry.geometryGroups[groupHash] = new GeoGroupStruct(new List<Face3>(), new List<Face4>(), materialIndex, 0, numMorphTargets, numMorphNormals);
                    }

                }

                if (face is Face3)
                {
                    GeoGroupStruct geogroupstruct = geometry.geometryGroups[groupHash];
                    geogroupstruct.faces3.Add((Face3)face);
                    geometry.geometryGroups[groupHash] = geogroupstruct;
                }
                else
                {
                    GeoGroupStruct geogroupstruct = geometry.geometryGroups[groupHash];
                    geogroupstruct.faces4.Add((Face4)face);
                    geometry.geometryGroups[groupHash] = geogroupstruct;
                }


                GeoGroupStruct geogroupstruc = geometry.geometryGroups[groupHash];
                geogroupstruc.vertices += vertices;
                geometry.geometryGroups[groupHash] = geogroupstruc;

                geometry.geometryGroupsList = new List<GeoGroupStruct>();

                foreach (string key in geometry.geometryGroups.Keys)
                {
                    GeoGroupStruct temp = geometry.geometryGroups[key];
                    temp.id = _geometryGroupCounter++;
                    geometry.geometryGroups[key] = temp;

                    geometry.geometryGroupsList.Add(geometry.geometryGroups[key]);
                }

            }
        }
	}
}

