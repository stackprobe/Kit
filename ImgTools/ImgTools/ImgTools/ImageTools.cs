using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTools
{
	public class ImageTools
	{
		public static DotData Mix(DotData[] dots)
		{
			long[] c = new long[] { 0, 0, 0, 0 }; // ARGB

			foreach (DotData dot in dots)
			{
				c[DotData.A] += dot.C[DotData.A];
			}
			if (c[DotData.A] == 0)
			{
				c[DotData.R] = 127;
				c[DotData.G] = 127;
				c[DotData.B] = 127;
			}
			else
			{
				for (int color = 1; color < 4; color++)
				{
					foreach (DotData dot in dots)
					{
						c[color] += dot.C[color] * dot.C[DotData.A];
					}
					c[color] = IntTools.DivRndOff(c[color], c[DotData.A]);
				}
				c[DotData.A] = IntTools.DivRndOff(c[DotData.A], dots.Length);
			}
			DotData mixedDot = new DotData();

			for (int color = 0; color < 4; color++)
			{
				mixedDot.C[color] = (int)c[color];
			}
			return mixedDot;
		}

		/// <summary>
		/// ドットの上にドットを重ねます。
		/// 背面と上に重ねる色のアルファ値を考慮します。
		/// </summary>
		/// <param name="ground">背面の色</param>
		/// <param name="surface">上に重ねる色</param>
		/// <returns>重ねた後の色</returns>
		public static DotData Paste(DotData ground, DotData surface)
		{
			int sa = surface.C[DotData.A];
			int ga = ground.C[DotData.A];

			ga *= 255 - sa;
			ga = (int)IntTools.DivRndOff(ga, 255);

			DotData ret = new DotData();

			ret.C[DotData.A] = sa + ga;

			for (int color = 1; color < 4; color++)
			{
				int sc = surface.C[color];
				int gc = ground.C[color];
				int c = sc * sa + gc * ga;

				c = (int)IntTools.DivRndOff(c, 255);

				ret.C[color] = c;
			}
			return ret;
		}
	}
}
