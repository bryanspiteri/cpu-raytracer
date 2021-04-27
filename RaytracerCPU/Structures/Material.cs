using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RaytracerCPU
{
	public abstract class Material
	{
		public virtual bool Scatter(Ray ray_in, HitRecord rec, out Vector3 attenuation, out Ray scattered) 
		{
			attenuation = new Vector3();
			scattered = null;
			return false;
		}
	}
}
