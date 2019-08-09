using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTools
{
	public static class IntTools
	{
		public static long DivRndOff(long numer, long denom, long rndOffRateNumer = 1, long rndOffRateDenom = 2)
		{
			long ans = numer / denom;
			long rem = numer % denom;

			rem *= rndOffRateDenom;
			rem /= rndOffRateNumer;
			rem /= denom;

			return ans + rem;
		}

		public static int ToInt(double value)
		{
			if (value < 0.0)
				return (int)(value - 0.5);
			else
				return (int)(value + 0.5);
		}
	}
}
