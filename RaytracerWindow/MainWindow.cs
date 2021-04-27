using RaytracerCPU;
using System;
using System.Threading;
using System.Windows.Forms;

namespace RaytracerWindow
{
	public partial class MainWindow : Form
	{
		private delegate void UpdateProgressDelegate(float percentageRendered);
		private delegate void EnableButtonDelegate(bool enable);

		public MainWindow()
		{
			InitializeComponent();
			heightBox.Text = Renderer.HEIGHT.ToString();
			widthBox.Text = Renderer.WIDTH.ToString();
			msaaCount.Text = Renderer.MSAA_SAMPLE_COUNT.ToString();
			bouncesTxt.Text = Renderer.MAX_DEPTH.ToString();
			threadCount.Text = Renderer.MAX_THREADS.ToString();
		}

		private void renderBtn_Click(object sender, EventArgs e)
		{
			// Pass the settings to the Renderer
			Renderer.WIDTH = int.Parse(widthBox.Text);
			Renderer.HEIGHT = int.Parse(heightBox.Text);
			Renderer.ASPECT_RATIO = Renderer.WIDTH / (double)Renderer.HEIGHT;
			Renderer.MSAA_SAMPLE_COUNT = int.Parse(msaaCount.Text);
			Renderer.MAX_DEPTH = int.Parse(bouncesTxt.Text);
			Renderer.MAX_THREADS = int.Parse(threadCount.Text);

			// Render on a separate thread
			var thread = new Thread(()=>
			{
				progressLabel.Invoke(new EnableButtonDelegate(EnableBtn), false);

				// Setup the renderer
				var renderer = new Renderer();
				renderer.Start();

				// Draw
				renderer.Render((float val) =>
				{
					// Callback to update progress
					progressLabel.Invoke(new UpdateProgressDelegate(UpdateProgress), val);
				});

				// Save
				renderer.Framebuffer.SaveToFile("outframe.png");

				// View?
				new RaytraceResultViewer(renderer, Renderer.WIDTH, Renderer.HEIGHT, "outframe.png").ShowDialog();

				progressLabel.Invoke(new EnableButtonDelegate(EnableBtn), true);
			});
			thread.IsBackground = true;
			thread.Start();
		}

		private void UpdateProgress(float value)
		{
			// Progress @ 2dp
			progressLabel.Text = value.ToString("n2") + "%";
			renderProgress.Value = (int) (value * 1000);
		}

		private void EnableBtn(bool value)
		{
			renderBtn.Enabled = value;
			if (value)
			{
				renderBtn.Text = "Render";
			}
			else
			{
				renderBtn.Text = "Please wait...";
			}
		}
	}
}
