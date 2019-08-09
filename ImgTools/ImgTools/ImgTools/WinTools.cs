using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ImgTools
{
	public static class WinTools
	{
		public static Bitmap PrintScreen()
		{
			Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
			}
			return bmp;
		}
	}
}
