using System;
using System.Numerics;
using System.Text;

using Color = System.Numerics.Vector3;

namespace RaytracerCPU
{
	public class DielectricMaterial : Material
	{
		public float ir;

		public DielectricMaterial(float ir)
		{
			this.ir = ir;
		}

		public override bool Scatter(Ray ray_in, HitRecord rec, out Vector3 attenuation, out Ray scattered)
		{
			attenuation = new Color(1f, 1f, 1f);
			float refraction_ratio = rec.front_face ? (1.0f / ir) : ir;

			Vector3 unit_direction = Vector3.Normalize(ray_in.direction);
			float cos_theta = (float) Math.Min(Vector3.Dot(-unit_direction, rec.normal), 1.0f);
			float sin_theta = (float) Math.Sqrt(1.0f - cos_theta * cos_theta);

			// total internal reflection
			bool cannot_refract = refraction_ratio * sin_theta > 1.0f;
			Vector3 direction;

			if (cannot_refract || Utils.Reflectance(cos_theta, refraction_ratio) > Renderer.random.NextDouble())
				direction = Vector3.Reflect(unit_direction, rec.normal);
			else
				direction = Utils.Refract(unit_direction, rec.normal, refraction_ratio);

			scattered = new Ray(rec.point, direction);
			return true;
		}
	}
}
