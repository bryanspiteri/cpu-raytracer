using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Color = System.Numerics.Vector3;

namespace RaytracerCPU
{
	public class Scene
	{
		private Renderer Renderer;
		public List<Hittable> hittables = new List<Hittable>();

		public Scene(Renderer renderer)
		{
			Renderer = renderer;
		}

		public virtual void Init()
		{

		}

		public virtual bool Draw(Ray ray, Vector3 unit_direction, int depth, out Color colorFinal)
		{
			// If we've exceeded the ray bounce limit, no more light is gathered.
			if (depth <= 0)
			{
				colorFinal = new Color(0, 0, 0);
				return false;
			}

			for (int i = hittables.Count - 1; i > -1; i--)
			{
				if (hittables[i].Hit(ray, 0.001f, float.PositiveInfinity, out var rec))
				{
					// spit out the normal lmao
					// colorFinal = 0.5f * (rec.normal + new Color(1, 1, 1));

					// color
					Ray scattered;
					Color attenuation;
					if (rec.material.Scatter(ray, rec, out attenuation, out scattered))
					{
						colorFinal = attenuation * Renderer.RayColor(scattered, depth - 1);
						return true;
					}

					colorFinal = new Color(0, 0, 0);
					return false;
				}
			}

			colorFinal = new Color(0, 0, 0);
			return false;
		}
	}
}
