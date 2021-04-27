using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RaytracerCPU
{
	public class SphereScene : Scene
	{
		public SphereScene(Renderer r) : base(r) { }

		public override void Init()
		{
			Material material_ground = new LambertianMaterial(new Vector3(0.8f, 0.8f, 0.0f));
			Material material_center = new LambertianMaterial(new Vector3(0.1f, 0.2f, 0.5f));
			Material material_left =   new DielectricMaterial(1.5f);
			Material material_right =  new MetalMaterial(new Vector3(0.8f, 0.6f, 0.2f), 0.0f);

			/*
			hittables.Add(new Sphere(new Vector3( 0.0f,    0.0f, -1.0f),   0.5f, new LambertianMaterial(new Vector3(0.8f, 0.8f, 0.0f))));
			hittables.Add(new Sphere(new Vector3( 1.0f,    0.0f, -1.0f),   0.5f, new DielectricMaterial(1.5f)));
			hittables.Add(new Sphere(new Vector3(-1.0f,    0.0f, -1.0f),   0.5f, new MetalMaterial(new Vector3(0.8f, 0.6f, 0.2f), 1.0f)));
			hittables.Add(new Sphere(new Vector3( 1.0f,    0.0f, -1.0f),   0.5f, new MetalMaterial(new Vector3(0.8f, 0.6f, 0.2f), 0.3f)));
			hittables.Add(new Sphere(new Vector3(0.0f, -100.5f, -1.0f), 100.0f, new LambertianMaterial(new Vector3(0.7f, 0.3f, 0.3f))));
			*/

			hittables.Add(new Sphere(new Vector3( 0.0f, -100.5f, -1.0f), 100.0f, material_ground));
			hittables.Add(new Sphere(new Vector3( 0.0f,    0.0f, -1.0f), 0.5f, material_center));
			hittables.Add(new Sphere(new Vector3(-1.0f,    0.0f, -1.0f), 0.5f, material_left));
			hittables.Add(new Sphere(new Vector3(-1.0f,    0.0f, -1.0f), 0.4f, material_left));
			hittables.Add(new Sphere(new Vector3( 1.0f,    0.0f, -1.0f), 0.5f, material_right));
		}
	}
}
