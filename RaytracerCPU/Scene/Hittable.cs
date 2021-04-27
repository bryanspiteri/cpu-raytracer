using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RaytracerCPU
{
	public struct HitRecord
	{
		public Vector3 point;
		public Vector3 normal;
		public float t;
		public bool front_face;
		public Material material;

		public HitRecord(Vector3 p, Vector3 n, float t)
		{
			point = p;
			normal = n;
			this.t = t;
			front_face = false;
			material = null;
		}

		public void set_face_normal(Ray ray, Vector3 outward_normal)
		{
			front_face = Vector3.Dot(ray.direction, outward_normal) < 0;
			normal = front_face ? outward_normal : -outward_normal;
		}
	}

	/// <summary>
	/// A generic class of items that a <see cref="Ray"/> may intersect with
	/// </summary>
	public abstract class Hittable
	{
		public virtual bool Hit(Ray ray, float t_min, float t_max, out HitRecord rec)
		{
			rec = new HitRecord();
			return false;
		}
	}
}
