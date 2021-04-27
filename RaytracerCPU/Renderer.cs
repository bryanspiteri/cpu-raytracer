using Serilog;
using Serilog.Core;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Color = System.Numerics.Vector3;

namespace RaytracerCPU
{
	public class Renderer
	{
		public static double ASPECT_RATIO = 16.0 / 9.0;
		public static int WIDTH = 1280;
		public static int HEIGHT = (int)(WIDTH / ASPECT_RATIO);
		const float DEG_TO_RAD = 0.01745329f;
		public static int MSAA_SAMPLE_COUNT = 100;
		public static int MAX_DEPTH = 50;
		public static int MAX_THREADS = 1;
		// The maximum size of a region region in pixels; a region region is a square
		const int REGION_MAX_SIZE = 128;

		public Texture2D Framebuffer;
		public EventHandler OnImageUpdate;

		private Camera camera;
		private Scene scene;

		public static Random random;
		public static Logger LOGGER;

		private Thread[] renderThreads;

		public Renderer()
		{
			LOGGER = new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.File("log.txt")
				.CreateLogger();

			random = new Random();

			Framebuffer = new Texture2D(this, WIDTH, HEIGHT);
			camera = new Camera();
			camera.ViewMatrix = Matrix4x4.CreatePerspectiveFieldOfView(DEG_TO_RAD * 90f, (float)ASPECT_RATIO, 0.1f, 1000f) *
				Matrix4x4.CreateLookAt(new Vector3(0, 0, 3), -Vector3.UnitZ, -Vector3.UnitY);
			camera.ComputePerspective(1);

			scene = new SphereScene(this);

			Start();
		}

		public void Start()
		{
			scene.Init();
		}

		public void Render(Action<float> progressCallback)
		{
			System.IO.Directory.CreateDirectory("tmp");

			/*
			// TODO: Divide the render region into squares of REGION_MAX_SIZE x REGION_MAX_SIZE pixels, and then batch them to threads
			renderThreads = new Thread[MAX_THREADS];

			// number of tiles in width and height
			int tilesX = (int) Math.Ceiling(Framebuffer.width / (float)REGION_MAX_SIZE);
			int tilesY = (int) Math.Ceiling(Framebuffer.height / (float)REGION_MAX_SIZE);

			RenderRegionData[,] framebuffers = new RenderRegionData[tilesX,tilesY];
			*/

			LOGGER.Information($"Rendering....");

			Stopwatch sw = new Stopwatch();

			sw.Start();

			if (MAX_THREADS != 1)
			{
				long lines = 0;

				Parallel.For(0, Framebuffer.height, new ParallelOptions { MaxDegreeOfParallelism = MAX_THREADS }, j =>
				{
					//LOGGER.Information($"Rendering line {j}...");

					for (int i = 0; i < Framebuffer.width; i++)
					{
						Color pixelColor = new Color(0, 0, 0);
						for (int s = 0; s < MSAA_SAMPLE_COUNT; s++)
						{
							// Calculate the ray that we're going to shoot
							double u = (double)(i + random.NextDouble()) / (Framebuffer.width - 1);
							double v = (double)(j + random.NextDouble()) / (Framebuffer.height - 1);
							Ray ray = camera.GetRay(u, v);

							// Get the color of the element the ray is going to hit	
							pixelColor += RayColor(ray, MAX_DEPTH);
						}
						Framebuffer.SetPixel(i, j, pixelColor);

						//LOGGER.Information($"Rendered pixel {i},{j}");
					}

					//LOGGER.Information($"Rendered line {j}!");

					Interlocked.Add(ref lines, 1);
					// Progress bar
					//LOGGER.Information($"(float){j} / ({Framebuffer.height} - 1f) * 100f");
					//progressCallback(Interlocked.Read(ref lines) / (Framebuffer.height - 1f) * 100f);
				});
			}
			else
			{
				for (int j = 0; j < Framebuffer.height; j++)
				{
					for (int i = 0; i < Framebuffer.width; i++)
					{
						Color pixelColor = new Color(0, 0, 0);
						for (int s = 0; s < MSAA_SAMPLE_COUNT; s++)
						{
							// Calculate the ray that we're going to shoot
							double u = (double)(i + random.NextDouble()) / (Framebuffer.width - 1);
							double v = (double)(j + random.NextDouble()) / (Framebuffer.height - 1);
							Ray ray = camera.GetRay(u, v);

							// Get the color of the element the ray is going to hit	
							pixelColor += RayColor(ray, MAX_DEPTH);
						}
						Framebuffer.SetPixel(i, j, pixelColor);
					}

					// Progress bar
					progressCallback((float)j / (Framebuffer.height - 1f) * 100f);
				}
			}

			progressCallback(100);
			Framebuffer.WritePixels();

			sw.Stop();
			LOGGER.Information($"Rendered in {sw.ElapsedMilliseconds} ms!");
		}

		// proxy for thread cuz the tuple is cursed
		/*private void DrawRegionThread(object data)
		{
			RenderRegionData dt = (RenderRegionData) data;
			DrawRegion(dt.regionX, dt.regionY, dt.progressCallback, out dt.tex);
		}*/

		// Draws a region of the scene
		//private void DrawRegion(int regionX, int regionY, Action<float> progressCallback, out Texture2D framebuffer)
		private void DrawRegion(object dt)
		{
			RenderRegionData data = (RenderRegionData) dt;

			// Calculate the boudns of the region
			int minX = Utils.ClampInt(REGION_MAX_SIZE *  data.regionX,      0, Framebuffer.width );
			int minY = Utils.ClampInt(REGION_MAX_SIZE *  data.regionY,      0, Framebuffer.height);
			int maxX = Utils.ClampInt(REGION_MAX_SIZE * (data.regionX + 1), 0, Framebuffer.width );
			int maxY = Utils.ClampInt(REGION_MAX_SIZE * (data.regionY + 1), 0, Framebuffer.height);

			data.tex = new Texture2D(this, maxX - minX, maxY - minY);

			// Declare a buffer for the pixels
			//Color[,] pixels = new Color[maxX - minX, maxY - minY];

			// Draw the scene at the specified region
			for (int j = minY; j < maxY; j++)
			{
				for (int i = minX; i < maxX; i++)
				{
					Color pixelColor = new Color(0, 0, 0);
					for (int s = 0; s < MSAA_SAMPLE_COUNT; s++)
					{
						// Calculate the ray that we're going to shoot
						double u = (double)(i + random.NextDouble()) / (Framebuffer.width - 1);
						double v = (double)(j + random.NextDouble()) / (Framebuffer.height - 1);
						Ray ray = camera.GetRay(u, v);

						// Get the color of the element the ray is going to hit	
						pixelColor += RayColor(ray, MAX_DEPTH);
					}
					data.tex.SetPixel(i - minX, j - minY, pixelColor);
					//pixels[i - minX, j - minY] = pixelColor;

					// Progress bar
					//progressCallback((float)j / Framebuffer.height * 100f);
				}
			}

			data.tex.SaveToFile($"tmp\\{data.regionX}-{data.regionY}_buf.png");

			// Write the pixel data
			//Framebuffer.CopyPixels(pixels, minX, minY, maxX, maxY);
		}

		public Color RayColor(Ray r, int depth)
		{
			Vector3 unit_direction = Vector3.Normalize(r.direction);

			if (scene.Draw(r, unit_direction, depth, out var col))
			{
				return col;
			}

			return Coloriser.GetSkybox(unit_direction);
		}
	}
}
