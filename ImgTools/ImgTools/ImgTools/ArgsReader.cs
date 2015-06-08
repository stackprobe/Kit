using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImgTools
{
	public class ArgsReader
	{
		private string[] _args;
		private int _rIndex;

		public ArgsReader(string[] args)
		{
			_args = args;
			_rIndex = 0;
		}

		public bool HasArgs()
		{
			return this.HasArgs(1);
		}

		public bool HasArgs(int count)
		{
			return _rIndex + count <= _args.Length;
		}

		public bool ArgIs(string spell)
		{
			if (this.HasArgs(1) && _args[_rIndex].ToLower() == spell.ToLower())
			{
				_rIndex++;
				return true;
			}
			return false;
		}

		public string NextArg()
		{
			return _args[_rIndex++];
		}
	}
}
