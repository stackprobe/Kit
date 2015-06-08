using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImgTools
{
	public class ImageData
	{
		private List<List<DotData>> _rows = new List<List<DotData>>();
		private int _w = 1;
		private int _h = 1;

		public ImageData()
		{ }

		public ImageData(int w, int h)
		{
			this.Resize(w, h);
		}

		public int Get_W()
		{
			return _w;
		}

		public int Get_H()
		{
			return _h;
		}

		public void Resize(int w, int h)
		{
			if (w < 1 || h < 1)
				throw new ArgumentException();

			_w = w;
			_h = h;
		}

		public DotData GetDot(int x, int y)
		{
			List<DotData> row;

			while (_rows.Count <= y)
			{
				_rows.Add(new List<DotData>());
			}
			row = _rows[y];

			while (row.Count <= x)
			{
				row.Add(new DotData());
			}
			return row[x];
		}

		public void Load(string rImgFile)
		{
			using (Image img = Image.FromFile(rImgFile))
			using (Bitmap bmp = new Bitmap(img))
			{
				this.Load(bmp);
			}
		}

		public void Load(Bitmap bmp)
		{
			this.Resize(bmp.Width, bmp.Height);

			for (int x = 0; x < _w; x++)
			{
				for (int y = 0; y < _h; y++)
				{
					Color c = bmp.GetPixel(x, y);
					DotData dot = this.GetDot(x, y);

					dot.C[DotData.A] = c.A;
					dot.C[DotData.R] = c.R;
					dot.C[DotData.G] = c.G;
					dot.C[DotData.B] = c.B;
				}
			}
		}

		public void Save(string wImgFile)
		{
			this.Save(wImgFile, ImageFormat.Png);
		}

		public void Save(string wImgFile, ImageFormat imgFmt)
		{
			using (Bitmap bmp = new Bitmap(_w, _h))
			{
				for (int x = 0; x < _w; x++)
				{
					for (int y = 0; y < _h; y++)
					{
						DotData dot = this.GetDot(x, y);

						bmp.SetPixel(x, y, Color.FromArgb(
							dot.C[DotData.A],
							dot.C[DotData.R],
							dot.C[DotData.G],
							dot.C[DotData.B]
							));
					}
				}
				bmp.Save(wImgFile, imgFmt);
			}
		}

		// ---- Tools ----

		public void Turn()
		{
			_rows.Reverse();
		}

		public void SetDot(int x, int y, DotData src)
		{
			this.GetDot(x, y).Set(src);
		}

		public void SetDotAll(DotData src)
		{
			for (int x = 0; x < _w; x++)
			{
				for (int y = 0; y < _h; y++)
				{
					this.SetDot(x, y, src);
				}
			}
		}

		// ----
	}
}
