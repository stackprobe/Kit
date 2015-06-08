using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgToCsv
{
	public class ArrayTools
	{
		public class Reader<T>
		{
			private T[] _src;
			private int _rPos;

			public Reader(T[] src)
			{
				_src = src;
				_rPos = 0;
			}

			public bool HasMore()
			{
				return _rPos < _src.Length;
			}

			public T Next()
			{
				return _src[_rPos++];
			}
		}
	}
}
