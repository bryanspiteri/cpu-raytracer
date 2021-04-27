using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Color = System.Numerics.Vector3;

namespace RaytracerCPU
{
	public class Coloriser
	{
		public static Color GetSkybox(Vector3 raydir)
		{
			var t = 0.5f * (raydir.Y + 1.0f);
			return (1.0f - t) * new Color(1.0f, 1.0f, 1.0f) + t * new Color(0.5f, 0.7f, 1.0f);
		}
	}
}
