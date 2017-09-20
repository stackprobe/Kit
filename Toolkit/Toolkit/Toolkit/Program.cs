using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

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
					byte[] hash;

					using (SHA512 sha512 = SHA512.Create())
					using (FileStream fs = new FileStream(rFile, FileMode.Open, FileAccess.Read))
					{
						hash = sha512.ComputeHash(fs);
					}
					using (Image img = Image.FromFile(rFile))
					using (Bitmap bmp = new Bitmap(img))
					using (Graphics g = Graphics.FromImage(bmp))
					{
						g.FillRectangle(
							new LinearGradientBrush(new Point(0, 0), new Point(bmp.Width, bmp.Height), Color.DarkGray, Color.DarkOrange),
							0,
							0,
							bmp.Width,
							bmp.Height
							);
						//g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height); // 単色

						DrawCode(g, bmp.Width, bmp.Height, hash);
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
				if (EqualsIgnoreCase(argq.Peek(), "/EVENT-LOG"))
				{
					// fixme -- 使うようになったら直そう...
					foreach (var el in EventLog.GetEventLogs())
					{
						foreach (EventLogEntry ele in el.Entries)
						{
							DateTime dt = ele.TimeGenerated;

							Console.WriteLine(
								dt.Year + "," +
								dt.Month + "," +
								dt.Day + "," +
								dt.Hour + "," +
								dt.Minute + "," +
								dt.Second
								);
						}
					}
				}
				// ここへ追加..
				throw new Exception("不明なオプション：" + argq.Peek());
			}
		}

		private void DrawCode(Graphics g, int gw, int gh, byte[] hash)
		{
			int dh = Math.Min(10, gh);

			for (int i = 0; i / 8 < hash.Length && i < gw; i++)
			{
				bool bit = (hash[i / 8] & (1 << (i % 8))) != 0;

				if (bit)
					g.DrawLine(new Pen(Color.Orange), new Point(i, 0), new Point(i, dh));
			}
		}

		private bool EqualsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}
	}
}
