using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RaytracerCPU
{
	public class Sphere : Hittable
	{
		public Vector3 center;
		public float radius;
		public Material material;

		public Sphere(Vector3 center, float radius, Material material)
		{
			this.center = center;
			this.radius = radius;
			this.material = material;
		}

		public override bool Hit(Ray ray, float t_min, float t_max, out HitRecord rec)
		{
			rec = new HitRecord();
			rec.material = null;

			Vector3 oc = ray.origin - center;
			float a = ray.direction.LengthSquared();
			float half_b = Vector3.Dot(oc, ray.direction);
			float c = oc.LengthSquared() - radius * radius;

			var discriminant = half_b * half_b - a * c;
			if (discriminant < 0)
			{
				return false;
			}
			float sqrtd = (float) Math.Sqrt(discriminant);

			// Find the nearest root that lies in the acceptable range.
			float root = (-half_b - sqrtd) / a;
			if (root < t_min || t_max < root)
			{
				root = (-half_b + sqrtd) / a;
				if (root < t_min || t_max < root)
				{
					return false;
				}
			}

			rec.t = root;
			rec.point = ray.PointAt((float)rec.t);
			rec.normal = (rec.point - center) / radius;
			Vector3 outward_normal = (rec.point - center) / radius;
			rec.set_face_normal(ray, outward_normal);
			rec.material = material;

			return true;
		}
	}
}
