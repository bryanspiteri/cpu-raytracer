using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RaytracerCPU
{
	public class Camera
	{
		// Camera
		public Matrix4x4 ViewMatrix;

		public Vector3 origin = Vector3.Zero;
		public Vector3 horizontal = Vector3.Zero;
		public Vector3 vertical = Vector3.Zero;
		public Vector3 lower_left_corner = Vector3.Zero;

		public void ComputePerspective(float focal_length)
		{
			float near = ViewMatrix.M34 / (ViewMatrix.M33 - 1);
			float far = ViewMatrix.M34 / (ViewMatrix.M33 + 1);
			float bottom = near * (ViewMatrix.M23 - 1) / ViewMatrix.M22;
			float top = near * (ViewMatrix.M23 + 1) / ViewMatrix.M22;
			float left = near * (ViewMatrix.M13 - 1) / ViewMatrix.M11;
			float right = near * (ViewMatrix.M13 + 1) / ViewMatrix.M11;

			origin = new Vector3(0, 0, 0);
			horizontal = 2 * new Vector3(right, 0, 0);
			vertical = 2 * new Vector3(0, top, 0);
			lower_left_corner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focal_length);
		}

		public Ray GetRay(double u, double v)
		{
			return new Ray(origin, lower_left_corner + (float) u * horizontal + (float) v * vertical - origin);
		}
	}
}
