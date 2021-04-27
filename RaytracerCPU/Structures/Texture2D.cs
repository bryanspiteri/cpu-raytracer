using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Drawing;

using SysBmp = System.Drawing.Bitmap;
using SysCol = System.Drawing.Color;
using Color = System.Numerics.Vector3;

namespace RaytracerCPU
{
	public unsafe class Texture2D
	{
		public int width;
		public int height;

		private Color[] pixels;
		public SysBmp sysBmpBuffer;
		public Renderer renderer;

		public Texture2D(Renderer renderer, int width, int height)
		{
			this.renderer = renderer;
			this.width = width;
			this.height = height;
			pixels = new Color[width * height];
			sysBmpBuffer = new SysBmp(width, height, PixelFormat.Format32bppRgb);
		}

		public void SetPixel(int x, int y, Color col)
		{
			// Average the sample count
			float scale = 1.0f / Renderer.MSAA_SAMPLE_COUNT;
			col.X = scale * col.X;
			col.Y = scale * col.Y;
			col.Z = scale * col.Z;

			col = Utils.GammaCorrect(col);

			// Write the color
			y = height - y - 1;
			pixels[y * width + x] = col;
		}

		public unsafe void WritePixels()
		{
			/*
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					sysBmpBuffer.SetPixel(i, j, SysCol.FromArgb(255,
						(byte)Utils.Clamp(pixels[i + j * width].X * 255, 0, 255),
						(byte)Utils.Clamp(pixels[i + j * width].Y * 255, 0, 255),
						(byte)Utils.Clamp(pixels[i + j * width].Z * 255, 0, 255)));
				}
			}

			return;
			*/

			unsafe
			{
				BitmapData bitmapData = sysBmpBuffer.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, sysBmpBuffer.PixelFormat);

				int bytesPerPixel = Image.GetPixelFormatSize(sysBmpBuffer.PixelFormat) / 8;
				int widthInBytes = bitmapData.Width * bytesPerPixel;
				byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

				Parallel.For(0, bitmapData.Height, new ParallelOptions { MaxDegreeOfParallelism = Renderer.MAX_THREADS }, y =>
				{
					byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
					for (int x = 0; x < widthInBytes; x += bytesPerPixel)
					{
						int xLocal = x / bytesPerPixel;

						currentLine[x] =     (byte)(Utils.Clamp(pixels[xLocal + y * width].Z * 255, 0, 255));
						currentLine[x + 1] = (byte)(Utils.Clamp(pixels[xLocal + y * width].Y * 255, 0, 255));
						currentLine[x + 2] = (byte)(Utils.Clamp(pixels[xLocal + y * width].X * 255, 0, 255));
					}
				});
				sysBmpBuffer.UnlockBits(bitmapData);
			}

			//renderer.OnImageUpdate?.Invoke(null, null);
		}

		public void SaveToFile(string path)
		{
			sysBmpBuffer.Save(path);
			sysBmpBuffer.Dispose();
		}

		/// <summary>
		/// Copies a pixel buffer to the specified area on the texture
		/// </summary>
		public void CopyPixels(Color[,] pixels, int left, int top, int right, int bottom)
		{
			float scale = 1.0f / Renderer.MSAA_SAMPLE_COUNT;

			unsafe
			{
				BitmapData bitmapData = sysBmpBuffer.LockBits(new Rectangle(0, 0, right - left, bottom - top), ImageLockMode.ReadWrite, sysBmpBuffer.PixelFormat);

				int bytesPerPixel = Image.GetPixelFormatSize(sysBmpBuffer.PixelFormat) / 8;
				int widthInBytes = bitmapData.Width * bytesPerPixel;
				byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

				Parallel.For(0, bitmapData.Height, new ParallelOptions { MaxDegreeOfParallelism = Renderer.MAX_THREADS }, y =>
				{
					byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
					for (int x = 0; x < widthInBytes; x += bytesPerPixel)
					{
						int xLocal = x / bytesPerPixel;

						//int oldBlue = currentLine[x];
						//int oldGreen = currentLine[x + 1];
						//int oldRed = currentLine[x + 2];

						//currentLine[x] = (byte)oldBlue;
						//currentLine[x + 1] = (byte)oldGreen;
						//currentLine[x + 2] = (byte)oldRed;

						// Average the sample count
						pixels[xLocal, y].X = scale * pixels[xLocal, y].X;
						pixels[xLocal, y].Y = scale * pixels[xLocal, y].Y;
						pixels[xLocal, y].Z = scale * pixels[xLocal, y].Z;

						pixels[xLocal, y] = Utils.GammaCorrect(pixels[xLocal, y]);

						currentLine[x] = (byte)(Utils.Clamp(pixels[xLocal, y].X, 0, 1) * 255);
						currentLine[x + 1] = (byte)(Utils.Clamp(pixels[xLocal, y].Y, 0, 1) * 255);
						currentLine[x + 2] = (byte)(Utils.Clamp(pixels[xLocal, y].Z, 0, 1) * 255);
					}
				});
				sysBmpBuffer.UnlockBits(bitmapData);
			}

			renderer.OnImageUpdate?.Invoke(null, null);
		}
	}
}
