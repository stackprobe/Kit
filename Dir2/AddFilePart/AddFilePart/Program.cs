using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AddFilePart
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
			AddFilePart(args[0], args[1], long.Parse(args[2]));
		}

		private static void AddFilePart(string rFile, string wFile, long startPos)
		{
			long size = new FileInfo(rFile).Length;

			using (FileStream rfs = new FileStream(rFile, FileMode.Open, FileAccess.Read))
			using (FileStream wfs = new FileStream(wFile, FileMode.OpenOrCreate, FileAccess.Write))
			{
				wfs.Seek(startPos, SeekOrigin.Begin);
				ReadWriteStream(rfs, wfs, size);
			}
		}

		private static void ReadWriteStream(FileStream rfs, FileStream wfs, long size)
		{
			byte[] buff = new byte[1024 * 1024 * 16];

			for (long count = 0; count < size; )
			{
				int rwSize = (int)Math.Min((long)buff.Length, size - count);
				int readSize = rfs.Read(buff, 0, rwSize);

				if (readSize != rwSize)
					throw new Exception("読み込みエラー: " + readSize + ", " + rwSize);

				wfs.Write(buff, 0, rwSize);
				count += rwSize;
			}
		}
	}
}
