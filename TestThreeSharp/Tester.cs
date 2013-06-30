using System;
using System.Collections.Generic;
using ThreeSharp;


namespace TestThreeSharp
{
	public class Tester
	{
		OpenGLRenderer renderer;
		PerspectiveCamera camera;
		Mesh mesh;
		Scene scene;


		int width = 800;
		int height = 600;


		public static void Main (String[] args)
		{



		}

		public void init ()
		{
			renderer = new OpenGLRenderer();
			renderer.setSize(width,height);

			camera = new PerspectiveCamera(70.0f,width/(float)height,1.0f,1000.0f);
			camera.Z_POSSITION = 400.0f;

			scene = new Scene();

			CubeGeometry geometry = new CubeGeometry(200.0f,200.0f,200.0f);


			Texture texture = ImageUtils.loadTexture("textures/crate.gif");
			texture.anisotropy = renderer.getMaxAnisotropy();

			Dictionary<string,object> parameters = new Dictionary<string, object>();
			parameters.Add("map",texture);
			MeshBasicMaterial material = new MeshBasicMaterial(parameters);

			mesh = new Mesh(geometry,material);
			scene.add(mesh);

			renderer.render(scene,camera);

		}




	}
}

