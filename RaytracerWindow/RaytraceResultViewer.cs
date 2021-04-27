using RaytracerCPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace RaytracerWindow
{
	public partial class RaytraceResultViewer : Form
	{
		private Renderer renderer;
		private delegate void UpdateImageDelegate();

		public RaytraceResultViewer(Renderer renderer, int width, int height, string path)
		{
			this.renderer = renderer;
			renderer.OnImageUpdate += UpdateImage;

			InitializeComponent(width, height);

			Bitmap img = new Bitmap(path);
			pictureBox1.Image = img;
		}

		public void UpdateImage(object sender, EventArgs e)
		{
			Console.WriteLine("update received!");
			pictureBox1.Invoke(new UpdateImageDelegate(UpdateImage));
		}

		private void UpdateImage()
		{
			BitmapData bitmapData = renderer.Framebuffer.sysBmpBuffer.LockBits(new Rectangle(0, 0, renderer.Framebuffer.sysBmpBuffer.Width, renderer.Framebuffer.sysBmpBuffer.Height), ImageLockMode.ReadWrite, renderer.Framebuffer.sysBmpBuffer.PixelFormat);
			pictureBox1.Image = renderer.Framebuffer.sysBmpBuffer;
			renderer.Framebuffer.sysBmpBuffer.UnlockBits(bitmapData);
		}
	}
}
