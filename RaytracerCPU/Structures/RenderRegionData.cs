using System;
using System.Collections.Generic;
using System.Text;

namespace RaytracerCPU
{
	public class RenderRegionData
	{
		public int regionX;
		public int regionY;
		public Action<float> progressCallback;
		public Texture2D tex;

		public RenderRegionData(int regionX, int regionY, Action<float> progressCallback, Texture2D tex)
		{
			this.regionX = regionX;
			this.regionY = regionY;
			this.progressCallback = progressCallback;
			this.tex = tex;
		}

		public RenderRegionData(int regionX, int regionY, Action<float> progressCallback)
		{
			this.regionX = regionX;
			this.regionY = regionY;
			this.progressCallback = progressCallback;
			this.tex = null;
		}
	}
}
