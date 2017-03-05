using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class DoubleTools
	{
		public static double toRange(double value, double minval, double maxval)
		{
			return Math.Min(
				Math.Max(value, minval),
				maxval
				);
		}
	}
}
