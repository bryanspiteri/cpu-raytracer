using System;
using System.Numerics;
using System.Text;

using Color = System.Numerics.Vector3;

namespace RaytracerCPU
{
	public class MetalMaterial : Material
	{
		public Color albedo;
		public float fuzz;

		public MetalMaterial(Color color, float fuzz)
		{
			albedo = color;
			this.fuzz = Math.Min(fuzz, 1.0f);
		}

		public override bool Scatter(Ray ray_in, HitRecord rec, out Vector3 attenuation, out Ray scattered)
		{
			Vector3 reflected = Vector3.Reflect(Vector3.Normalize(ray_in.direction), rec.normal);
			scattered = new Ray(rec.point, reflected + fuzz * Utils.RandomInUnitSphere());
			attenuation = albedo;
			return (Vector3.Dot(scattered.direction, rec.normal) > 0);
		}
	}
}
