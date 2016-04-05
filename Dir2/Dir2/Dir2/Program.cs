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
				if (args[0].ToUpper() == "//R")
				{
					Main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					Main2(args);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static void Main2(string[] args)
		{
			PrintDir(args[0], args[1], args[2]);
		}

		private static void PrintDir(string dir, string wFile, string successfulFile)
		{
			using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			{
				DirectoryInfo dirInfo = new DirectoryInfo(dir);

				foreach (FileInfo fi in dirInfo.GetFiles())
				{
					WriteLine(wfs, ZPad("" + fi.Length, 19) + "*" + ToString(fi.LastWriteTime) + "*F*" + fi.Name);
				}
				foreach (DirectoryInfo di in dirInfo.GetDirectories())
				{
					WriteLine(wfs, "0000000000000000000*20000101000000000*D*" + di.Name);
				}
			}
			CreateFile(successfulFile);
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

		private static Encoding Encoding_SJIS = Encoding.GetEncoding(932);
		//private static byte CR = 0x0d;
		private static byte LF = 0x0a;

		private static void WriteLine(FileStream wfs, string line)
		{
			Write(wfs, Encoding_SJIS.GetBytes(line));
			//wfs.WriteByte(CR);
			wfs.WriteByte(LF);
		}

		private static void Write(FileStream wfs, byte[] block)
		{
			wfs.Write(block, 0, block.Length);
		}

		private static void CreateFile(string wFile)
		{
			using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			{ }
		}
	}
}
