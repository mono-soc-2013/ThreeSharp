using System;

namespace ThreeSharp
{
	public class Three
	{
		//side
		public const int FrontSide = 0;
		public const int BackSide = 1;
		public const int DoubleSide = 2;

		// Wrapping modes
		public const int ClampToEdgeWrapping = 1001;

		//Filters 
		public const int LinearFilter = 1006;
		public const int LinearMipMapLinearFilter = 1008;

		// Data types
		public const int UnsignedByteType = 1009;

		// Pixel formats
		public const int RGBAFormat = 1021;

		// blending modes
		public const int NoBlending = 0;
		public const int NormalBlending = 1;
		public const int AdditiveBlending = 2;
		public const int SubtractiveBlending = 3;
		public const int MultiplyBlending = 4;
		public const int CustomBlending = 5;

		// custom blending destination factors
		public const int ZeroFactor = 200;
		public const int OneFactor = 201;
		public const int SrcColorFactor = 202;
		public const int OneMinusSrcColorFactor = 203;
		public const int SrcAlphaFactor = 204;
		public const int OneMinusSrcAlphaFactor = 205;
		public const int DstAlphaFactor = 206;
		public const int OneMinusDstAlphaFactor = 207;


		// custom blending equations
		// (numbers start from 100 not to clash with other
		//  mappings to OpenGL constants defined in Texture.js)

		public const int AddEquation = 100;
		public const int SubtractEquation = 101;
		public const int ReverseSubtractEquation = 102;

		// TEXTURE CONSTANTS

		public const int MultiplyOperation = 0;
		public const int MixOperation = 1;
		public const int AddOperation = 2;

		// shading

		public const int NoShading = 0;
		public const int FlatShading = 1;
		public const int SmoothShading = 2;

		// colors

		public const int NoColors = 0;
		public const int FaceColors = 1;
		public const int VertexColors = 2;

		// GL STATE CONSTANTS

		public const int CullFaceNone = 0;
		public const int CullFaceBack = 1;
		public const int CullFaceFront = 2;
		public const int CullFaceFrontBack = 3;

		// SHADOWING TYPES

		public const int BasicShadowMap = 0;
		public const int PCFShadowMap = 1;
		public const int PCFSoftShadowMap = 2;

		public Three ()
		{

		}


		public class UVMapping
		{

		}


	}
}

