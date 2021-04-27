using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RaytracerCPU
{
	public class Utils
	{
		public static double Clamp(double x, double min, double max)
		{
			if (x < min) return min;
			if (x > max) return max;
			return x;
		}
		
		public static int ClampInt(int x, int min, int max)
		{
			if (x < min) return min;
			if (x > max) return max;
			return x;
		}

		public static Vector3 RandomVec3()
		{
			return new Vector3(RandomFloat(), RandomFloat(), RandomFloat());
		}

		public static bool Vec3NearZero(Vector3 vec)
		{
			// Return true if the vector is close to zero in all dimensions.
			const double s = Math.E - 8;
			return (Math.Abs(vec.X) < s) && (Math.Abs(vec.Y) < s) && (Math.Abs(vec.Z) < s);
		}

		public static Vector3 Refract(Vector3 uv, Vector3 normal, float etai_over_etat)
		{
			float cos_theta = (float) Math.Min(Vector3.Dot(-uv, normal), 1.0);
			Vector3 r_out_perp = etai_over_etat * (uv + cos_theta * normal);
			Vector3 r_out_parallel = (float) -Math.Sqrt(Math.Abs(1.0f - r_out_perp.LengthSquared())) * normal;
			return r_out_perp + r_out_parallel;
		}

		public static Vector3 RandomVec3(double min, double max)
		{
			return new Vector3(RandomFloat(min, max), RandomFloat(min, max), RandomFloat(min, max));
		}

		public static float RandomFloat()
		{
			return (float)Renderer.random.NextDouble();
		}

		public static float RandomFloat(double min, double max)
		{
			return (float)(Renderer.random.NextDouble() * (max - min) + min);
		}

		public static Vector3 RandomInUnitSphere()
		{
			// optimized?
			const float delta = float.Epsilon * 5;
			var p = RandomVec3(-1, 1);
			if (p.LengthSquared() >= 1)
			{
				p = Vector3.Normalize(p);
				p -= delta * p;
			}
			return p;
			/*
			while (true)
			{
				var p = RandomVec3(-1, 1);
				if (p.LengthSquared() >= 1)
					continue;
				return p;
			}
			*/
		}

		public static Vector3 RandomUnitVector()
		{
			return Vector3.Normalize(RandomInUnitSphere());
		}

		public static Vector3 RandomInHemisphere(Vector3 normal) {
			Vector3 in_unit_sphere = RandomInUnitSphere();
			if (Vector3.Dot(in_unit_sphere, normal) > 0.0) // In the same hemisphere as the normal
				return in_unit_sphere;
			else
				return -in_unit_sphere;
		}

		public static Vector3 GammaCorrect(Vector3 inColor, float gamma = 2.2f)
		{
			return new Vector3(
				(float)Math.Pow(inColor.X, 1 / gamma),
				(float)Math.Pow(inColor.Y, 1 / gamma),
				(float)Math.Pow(inColor.Z, 1 / gamma));
		}

		public static float Reflectance(float cosine, float ref_idx)
		{
			// Use Schlick's approximation for reflectance.
			var r0 = (1 - ref_idx) / (1 + ref_idx);
			r0 = r0 * r0;
			return r0 + (1 - r0) * (float) Math.Pow((1 - cosine), 5);
		}
	}
}
