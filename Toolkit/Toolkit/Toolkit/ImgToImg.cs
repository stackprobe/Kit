using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace Toolkit
{
	public class ImgToImg
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rFile"></param>
		/// <param name="wFile"></param>
		/// <param name="q">-1 or 0 ～ 100</param>
		public void Perform(string rFile, string wFile, int q)
		{
			if (q < -1 || 100 < q)
				throw new Exception("q is out of range. q = " + q + " (range: -1 or 0 ～ 100)");

			using (Image img = Image.FromFile(rFile))
			using (Bitmap bmp = new Bitmap(img))
			{
				ImageFormat imgFmt = this.PathToImageFormat(wFile);

				if (imgFmt == ImageFormat.Jpeg && q != -1)
				{
					EncoderParameters eps = new EncoderParameters(1);
					eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)q);
					ImageCodecInfo ici = GetICI(imgFmt);
					bmp.Save(wFile, ici, eps);
				}
				else
				{
					bmp.Save(wFile, imgFmt);
				}
			}
		}

		private ImageCodecInfo GetICI(ImageFormat imgFmt)
		{
			return (from ici in ImageCodecInfo.GetImageEncoders() where ici.FormatID == imgFmt.Guid select ici).ToList()[0];
		}

		private ImageFormat PathToImageFormat(string path)
		{
			switch (Path.GetExtension(path).ToLower())
			{
				case ".bmp":
					return ImageFormat.Bmp;

				case ".emf":
					return ImageFormat.Emf;

				case ".exif":
					return ImageFormat.Exif;

				case ".gif":
					return ImageFormat.Gif;

				case ".ico":
					return ImageFormat.Icon;

				case ".jpeg":
				case ".jpg":
					return ImageFormat.Jpeg;

				case ".mbmp":
					return ImageFormat.MemoryBmp;

				case ".png":
					return ImageFormat.Png;

				case ".tif":
				case ".tiff":
					return ImageFormat.Tiff;

				case ".wmf":
					return ImageFormat.Wmf;

				default:
					throw new Exception("不明な拡張子");
			}
		}
	}
}
