using System;
using System.Drawing;


namespace ThreeSharp
{
	public class Light
	{
		public Color color;

		public Light (String hex)
		{
			ColorConverter converter = new ColorConverter();
			color = (Color)converter.ConvertFromString(hex);
		}

		public Light clone (Light light)
		{
			if (light == null) {
				light = new Light(null);
			}


			return null;
		}
	}
}

