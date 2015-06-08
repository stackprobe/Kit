using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTools
{
	public class DotData
	{
		public const int A = 0;
		public const int R = 1;
		public const int G = 2;
		public const int B = 3;

		public Color C = new Color();

		public class Color
		{
			UInt32 _c;

			public int this[int index]
			{
				get
				{
					return (int)((_c >> (index * 8)) & 0xff);
				}
				set
				{
					_c &= ~((UInt32)0xff << (index * 8));
					_c |= (UInt32)(value & 0xff) << (index * 8);
				}
			}
		}

		public DotData()
		{ }

		public DotData(int a, int r, int g, int b)
		{
			this.C[A] = a;
			this.C[R] = r;
			this.C[G] = g;
			this.C[B] = b;
		}

		public void Set(DotData src)
		{
			this.C[0] = src.C[0];
			this.C[1] = src.C[1];
			this.C[2] = src.C[2];
			this.C[3] = src.C[3];
		}
	}
}
