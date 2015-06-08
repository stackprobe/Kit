using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ImgToCsv
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void Main2(string[] args)
		{
			string rFile = args[0];
			string wFile = args[1];

			if (this.IsCsvPath(rFile)) // ? csv ->
			{
				if (this.IsCsvPath(wFile)) // ? csv -> csv
				{
					File.Copy(rFile, wFile);
				}
				else // ? csv -> img
				{
					this.CsvToImg(rFile, wFile);
				}
			}
			else // ? img ->
			{
				if (this.IsCsvPath(wFile)) // ? img -> csv
				{
					this.ImgToCsv(rFile, wFile);
				}
				else // ? img -> img
				{
					string midFile = FileTools.GetTempPath() + "_csv.tmp";

					try
					{
						this.ImgToCsv(rFile, midFile);
						this.CsvToImg(midFile, wFile);
					}
					finally
					{
						FileTools.DeleteFile(midFile);
					}
				}
			}
		}

		private bool IsCsvPath(string path)
		{
			return Path.GetExtension(path).ToLower() == ".csv";
		}

		private void ImgToCsv(string rFile, string wFile)
		{
			List<string[]> rows = new List<string[]>();

			using (Image img = Image.FromFile(rFile))
			using (Bitmap bmp = new Bitmap(img))
			{
				int w = bmp.Width;
				int h = bmp.Height;

				for (int y = 0; y < h; y++)
				{
					List<string> row = new List<string>();

					for (int x = 0; x < w; x++)
					{
						Color dot = bmp.GetPixel(x, y);
						StringBuilder buff = new StringBuilder();

						StringTools.AddHex(buff, dot.A);
						StringTools.AddHex(buff, dot.R);
						StringTools.AddHex(buff, dot.G);
						StringTools.AddHex(buff, dot.B);

						row.Add(buff.ToString());
					}
					rows.Add(row.ToArray());
				}
			}
			new CsvFile(wFile, Encoding.ASCII).WriteRows(rows.ToArray());
		}

		private void CsvToImg(string rFile, string wFile)
		{
			string[][] rows = new CsvFile(rFile, Encoding.ASCII).ReadToEnd();
			CsvFile.Trim(rows);

			using (Bitmap bmp = new Bitmap(rows[0].Length, rows.Length))
			{
				for (int rowidx = 0; rowidx < rows.Length; rowidx++)
				{
					string[] row = rows[rowidx];

					for (int colidx = 0; colidx < row.Length; colidx++)
					{
						string cell = row[colidx];
						ArrayTools.Reader<char> buff = new ArrayTools.Reader<char>(cell.ToCharArray());

						int a = StringTools.ReadHex(buff);
						int r = StringTools.ReadHex(buff);
						int g = StringTools.ReadHex(buff);
						int b = StringTools.ReadHex(buff);

						Color dot = Color.FromArgb(a, r, g, b);

						bmp.SetPixel(colidx, rowidx, dot);
					}
				}
				bmp.Save(wFile, this.PathToImageFormat(wFile));
			}
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
