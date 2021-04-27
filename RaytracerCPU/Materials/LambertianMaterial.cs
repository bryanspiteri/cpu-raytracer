using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using Color = System.Numerics.Vector3;

namespace RaytracerCPU
{
	public class LambertianMaterial : Material
	{
		public Color albedo;

		public LambertianMaterial(Color color)
		{
			albedo = color;
		}

		public override bool Scatter(Ray ray_in, HitRecord rec, out Vector3 attenuation, out Ray scattered)
		{
			Vector3 scatter_direction = rec.normal + Utils.RandomUnitVector();
			
			// Catch degenerate scatter direction
			if (Utils.Vec3NearZero(scatter_direction))
				scatter_direction = rec.normal;

			scattered = new Ray(rec.point, scatter_direction);
			attenuation = albedo;
			return true;
		}
	}
}
