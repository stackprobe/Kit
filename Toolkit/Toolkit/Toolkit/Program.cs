using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Toolkit
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
			Queue<string> argq = new Queue<string>(args);

			while (1 <= argq.Count)
			{
				if (EqualsIgnoreCase(argq.Peek(), "/MASK-RESOURCE-IMAGE"))
				{
					argq.Dequeue();
					string rFile = argq.Dequeue();
					string wFile = argq.Dequeue();

					using (Image img = Image.FromFile(rFile))
					using (Bitmap bmp = new Bitmap(img))
					using (Graphics g = Graphics.FromImage(bmp))
					{
						g.FillRectangle(
							new LinearGradientBrush(new Point(0, 0), new Point(bmp.Width, bmp.Height), Color.DarkRed, Color.White),
							0,
							0,
							bmp.Width,
							bmp.Height
							);
						//g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
						bmp.Save(wFile);
					}
					continue;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/IMG-TO-IMG"))
				{
					argq.Dequeue();
					string rFile = argq.Dequeue();
					string wFile = argq.Dequeue();
					int q = -1;

					if (1 <= argq.Count)
						q = int.Parse(argq.Dequeue()); // 0 ～ 100

					new ImgToImg().Perform(rFile, wFile, q);

					continue;
				}
				// ここへ追加..
				throw new Exception("不明なオプション：" + argq.Peek());
			}
		}

		private bool EqualsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}
	}
}
