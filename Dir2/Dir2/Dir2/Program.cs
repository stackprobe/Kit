using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Dir2
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static void Main2(string[] args)
		{
			PrintDir(args[0]);
		}

		private static void PrintDir(string dir)
		{
			DirectoryInfo dirInfo = new DirectoryInfo(dir);

			foreach (FileInfo fi in dirInfo.GetFiles())
			{
				Console.WriteLine(ZPad("" + fi.Length, 19) + "*" + ToString(fi.LastWriteTime) + "*F*" + fi.Name);
			}
			foreach (DirectoryInfo di in dirInfo.GetDirectories())
			{
				Console.WriteLine("0000000000000000000*20000101000000000*D*" + di.Name);
			}
		}

		private static string ToString(DateTime dt)
		{
			return
				ZPad("" + dt.Year, 4) +
				ZPad("" + dt.Month, 2) +
				ZPad("" + dt.Day, 2) +
				ZPad("" + dt.Hour, 2) +
				ZPad("" + dt.Minute, 2) +
				ZPad("" + dt.Second, 2) +
				ZPad("" + dt.Millisecond, 3);
		}

		private static string ZPad(string str, int minlen)
		{
			while (str.Length < minlen)
			{
				str = "0" + str;
			}
			return str;
		}
	}
}
