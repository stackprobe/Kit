using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PrintImage
{
	public static class Utils
	{
		public static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static bool equalsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}

		public static Rectangle adjustInside(int w, int h, Rectangle screen)
		{
			int screen_l = screen.Left;
			int screen_t = screen.Top;
			int screen_w = screen.Width;
			int screen_h = screen.Height;

			{
				int new_w = screen_w;
				int new_h = (h * screen_w) / w;

				if (screen_h < new_h)
				{
					new_w = (w * screen_h) / h;
					new_h = screen_h;

					if (screen_w < new_w)
						throw null;
				}
				w = new_w;
				h = new_h;
			}

			int l = screen_l + (screen_w - w) / 2;
			int t = screen_t + (screen_h - h) / 2;

			return new Rectangle(l, t, w, h);
		}
	}
}
