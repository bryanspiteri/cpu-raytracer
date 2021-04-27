using System.Numerics;

namespace RaytracerCPU
{
	public class Ray
	{
		public Vector3 origin;
		public Vector3 direction;

		public Ray(Vector3 origin, Vector3 direction)
		{
			this.origin = origin;
			this.direction = direction;
		}

		/// <summary>
		/// Returns a point on the ray relative to the origin by <c>t</c>
		/// </summary>
		/// <param name="t">How far along the ray to travel</param>
		/// <returns>A point on the ray</returns>
		public Vector3 PointAt(float t)
		{
			return origin + direction * t;
		}
	}
}
