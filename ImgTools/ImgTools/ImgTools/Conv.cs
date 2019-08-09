using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTools
{
	public static class Conv
	{
		public static ImageData Cut(ImageData src, int l, int t, int w, int h)
		{
			ImageData dest = new ImageData(w, h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					dest.GetDot(x, y).Set(src.GetDot(l + x, t + y));
				}
			}
			return dest;
		}

		public static ImageData Twist(ImageData src) // 右上と左下を反転
		{
			ImageData dest = new ImageData(src.Get_H(), src.Get_W());

			for (int x = 0; x < src.Get_W(); x++)
			{
				for (int y = 0; y < src.Get_H(); y++)
				{
					dest.GetDot(y, x).Set(src.GetDot(x, y));
				}
			}
			return dest;
		}

		public static ImageData Copy(ImageData src)
		{
			return Conv.Cut(src, 0, 0, src.Get_W(), src.Get_H());
		}

		public static ImageData Turn(ImageData src) // 上下反転
		{
			ImageData dest = Conv.Copy(src);
			dest.Turn();
			return dest;
		}

		/// <summary>
		/// 時計回りに回転する。
		/// </summary>
		/// <param name="src">元画像</param>
		/// <param name="rot">1～3:rot*90度回転する</param>
		/// <returns>回転した画像</returns>
		public static ImageData Rotate(ImageData src, int rot)
		{
			ImageData dest = src;

			switch (rot)
			{
				case 1:
					dest = Conv.Turn(dest);
					dest = Conv.Twist(dest);
					break;

				case 2:
					dest = Conv.Turn(dest);
					dest = Conv.Twist(dest);
					dest = Conv.Turn(dest);
					dest = Conv.Twist(dest);
					break;

				case 3:
					dest = Conv.Twist(dest);
					dest = Conv.Turn(dest);
					break;

				default:
					throw new Exception();
			}
			return dest;
		}

		public static ImageData LRTurn(ImageData src) // 左右反転
		{
			ImageData dest = src;

			dest = Conv.Twist(dest);
			dest = Conv.Turn(dest);
			dest = Conv.Twist(dest);

			return dest;
		}

		/// <summary>
		/// アルファ値を「無視して」貼り付ける。
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="dest_l"></param>
		/// <param name="dest_t"></param>
		/// <param name="src"></param>
		/// <param name="src_l"></param>
		/// <param name="src_t"></param>
		public static void Overwrite(ImageData dest, int dest_l, int dest_t, ImageData src, int src_l = 0, int src_t = 0)
		{
			Conv.Overwrite(dest, dest_l, dest_t, src, src_l, src_t, src.Get_W() - src_l, src.Get_H() - src_t);
		}

		/// <summary>
		/// アルファ値を「無視して」貼り付ける。
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="dest_l"></param>
		/// <param name="dest_t"></param>
		/// <param name="src"></param>
		/// <param name="src_l"></param>
		/// <param name="src_t"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public static void Overwrite(ImageData dest, int dest_l, int dest_t, ImageData src, int src_l, int src_t, int w, int h)
		{
			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					dest.GetDot(dest_l + x, dest_t + y).Set(src.GetDot(src_l + x, src_t + y));
				}
			}
		}

		public static ImageData Mirror(ImageData src, int dir)
		{
			ImageData dest = new ImageData();

			if (dir % 6 == 2) // ? 上下
				dest.Resize(src.Get_W(), src.Get_H() * 2);
			else // ? 左右
				dest.Resize(src.Get_W() * 2, src.Get_H());

			switch (dir)
			{
				case 2: // 下
					Conv.Overwrite(dest, 0, 0, src);
					Conv.Overwrite(dest, 0, src.Get_H(), Conv.Turn(src));
					break;

				case 4: // 左
					Conv.Overwrite(dest, src.Get_W(), 0, src);
					Conv.Overwrite(dest, 0, 0, Conv.LRTurn(src));
					break;

				case 6: // 右
					Conv.Overwrite(dest, 0, 0, src);
					Conv.Overwrite(dest, src.Get_W(), 0, Conv.LRTurn(src));
					break;

				case 8: // 上
					Conv.Overwrite(dest, 0, src.Get_H(), src);
					Conv.Overwrite(dest, 0, 0, Conv.Turn(src));
					break;

				default:
					throw new Exception();
			}
			return dest;
		}

		public static void TransPoint(ImageData img, int trans_x, int trans_y)
		{
			DotData trans_dot = img.GetDot(trans_x, trans_y);

			TransColor(img, trans_dot.C[DotData.R], trans_dot.C[DotData.G], trans_dot.C[DotData.B]);
		}

		public static void TransColor(ImageData img, int trans_r, int trans_g, int trans_b)
		{
			for (int x = 0; x < img.Get_W(); x++)
			{
				for (int y = 0; y < img.Get_H(); y++)
				{
					DotData dot = img.GetDot(x, y);

					if (
						dot.C[DotData.R] == trans_r &&
						dot.C[DotData.G] == trans_g &&
						dot.C[DotData.B] == trans_b
						)
						dot.C[DotData.A] = 0;
				}
			}
		}

		public static ImageData Rotate(ImageData src, double angle, double centralX, double centralY, DotData dummyDot, int dotDiv)
		{
			ImageData dest = new ImageData(src.Get_W(), src.Get_H());

			for (int x = 0; x < src.Get_W(); x++)
			{
				for (int y = 0; y < src.Get_H(); y++)
				{
					DotData[] srcDots = new DotData[dotDiv * dotDiv];
					int c = 0;

					for (int cx = 0; cx < dotDiv; cx++)
					{
						for (int cy = 0; cy < dotDiv; cy++)
						{
							double dx = x + (cx + 0.5) / dotDiv;
							double dy = y + (cy + 0.5) / dotDiv;

							dx -= centralX;
							dy -= centralY;

							{
								double dw;

								dw = dx * Math.Cos(angle) - dy * Math.Sin(angle);
								dy = dx * Math.Sin(angle) + dy * Math.Cos(angle);
								dx = dw;
							}

							dx += centralX;
							dy += centralY;

							int ix = (int)dx;
							int iy = (int)dy;

							DotData srcDot;

							if (
								0 <= ix && ix < src.Get_W() &&
								0 <= iy && iy < src.Get_H()
								)
								srcDot = src.GetDot(ix, iy);
							else
								srcDot = dummyDot;

							srcDots[c++] = srcDot;
						}
					}

					DotData mixedDot = ImageTools.Mix(srcDots);

					dest.GetDot(x, y).Set(mixedDot);
				}
			}
			return dest;
		}

		public static ImageData Extend(ImageData src, int l, int t, int r, int b, DotData dummyDot)
		{
			int w = l + src.Get_W() + r;
			int h = t + src.Get_H() + b;

			ImageData dest = new ImageData(w, h);
			dest.SetDotAll(dummyDot);
			Overwrite(dest, l, t, src);
			return dest;
		}

		/// <summary>
		/// アルファ値を「考慮して」貼り付ける。
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="deat_l"></param>
		/// <param name="deat_t"></param>
		/// <param name="src"></param>
		public static void Paste(ImageData dest, int dest_l, int dest_t, ImageData src)
		{
			for (int x = 0; x < src.Get_W(); x++)
			{
				for (int y = 0; y < src.Get_H(); y++)
				{
					DotData destDot = dest.GetDot(dest_l + x, dest_t + y);
					DotData srcDot = src.GetDot(x, y);

					destDot = ImageTools.Paste(destDot, srcDot);

					dest.SetDot(dest_l + x, dest_t + y, destDot);
				}
			}
		}

		public static ImageData Bokashi(ImageData src, int l, int t, int r, int b, int level, int mode)
		{
			ImageData dest;

			switch (mode)
			{
				case 0:
					dest = BokashiMain(src, l, t, r, b, level);
					break;

				case 1:
					dest = src;
					for (int c = 0; c < level; c++)
					{
						dest = BokashiMain(dest, l, t, r, b, 1);
					}
					break;

				default:
					throw new Exception("不明なモードです。" + mode);
			}
			return dest;
		}

		private static ImageData BokashiMain(ImageData src, int l, int t, int r, int b, int level)
		{
			ImageData dest = new ImageData(src.Get_W(), src.Get_H());

			for (int x = 0; x < src.Get_W(); x++)
			{
				for (int y = 0; y < src.Get_H(); y++)
				{
					DotData dot = src.GetDot(x, y);

					if (l <= x && x <= r && t <= y && y <= b)
					{
						List<DotData> dd = new List<DotData>();

						for (int cx = -level; cx <= level; cx++)
						{
							for (int cy = -level; cy <= level; cy++)
							{
								int dx = x + cx;
								int dy = y + cy;

								if (0 <= dx && dx < src.Get_W() && 0 <= dy && dy < src.Get_H())
								{
									DotData d = src.GetDot(dx, dy);

									dd.Add(d);
								}
							}
						}
						dot = ImageTools.Mix(dd.ToArray());
					}
					dest.SetDot(x, y, dot);
				}
			}
			return dest;
		}

		public static ImageData DotFltr(ImageData src, string aCmd, string rCmd, string gCmd, string bCmd)
		{
			ImageData dest = new ImageData(src.Get_W(), src.Get_H());

			for (int x = 0; x < src.Get_W(); x++)
			{
				for (int y = 0; y < src.Get_H(); y++)
				{
					DotData rDot = src.GetDot(x, y);
					DotData wDot = new DotData(
						DotFltr(rDot, aCmd),
						DotFltr(rDot, rCmd),
						DotFltr(rDot, gCmd),
						DotFltr(rDot, bCmd)
						);

					dest.SetDot(x, y, wDot);
				}
			}
			return dest;
		}

		private static int DotFltr(DotData rDot, string cmd)
		{
			cmd = cmd.ToUpper();

			string fmt = StringTools.ToFormatDig(cmd);

			switch (fmt)
			{
				case "A": return rDot.C[DotData.A];
				case "R": return rDot.C[DotData.R];
				case "G": return rDot.C[DotData.G];
				case "B": return rDot.C[DotData.B];
				case "AVG_RGB": return (int)IntTools.DivRndOff(rDot.C[DotData.R] + rDot.C[DotData.G] + rDot.C[DotData.B], 3);
				case "A_REV": return 255 - rDot.C[DotData.A];
				case "R_REV": return 255 - rDot.C[DotData.R];
				case "G_REV": return 255 - rDot.C[DotData.G];
				case "B_REV": return 255 - rDot.C[DotData.B];
				case "AVG_RGB_REV": return 255 - (int)IntTools.DivRndOff(rDot.C[DotData.R] + rDot.C[DotData.G] + rDot.C[DotData.B], 3);
				case "A:99": return DotFltr_Pct(rDot.C[DotData.A], cmd);
				case "R:99": return DotFltr_Pct(rDot.C[DotData.R], cmd);
				case "G:99": return DotFltr_Pct(rDot.C[DotData.G], cmd);
				case "B:99": return DotFltr_Pct(rDot.C[DotData.B], cmd);
			}
			return int.Parse(cmd);
		}

		private static int DotFltr_Pct(int color, string cmd)
		{
			return IntTools.ToInt(color * int.Parse(cmd.Substring(2)) / 100.0);
		}
	}
}
