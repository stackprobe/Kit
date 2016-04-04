using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GetFilePart
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
			GetFilePart(args[0], args[1], long.Parse(args[2]), long.Parse(args[3]), args[4]);
		}

		private static void GetFilePart(string rFile, string wFile, long startPos, long readSize, string successfulFile)
		{
			long rSize = new FileInfo(rFile).Length;

			if (
				startPos < 0 ||
				readSize < 0 ||
				rSize < startPos ||
				rSize - startPos < readSize
				)
				throw new Exception("読み込み開始位置及びサイズエラー: " + startPos + ", " + readSize + ", " + rSize);

			using (FileStream rfs = new FileStream(rFile, FileMode.Open, FileAccess.Read))
			using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			{
				rfs.Seek(startPos, SeekOrigin.Begin);
				ReadWriteStream(rfs, wfs, readSize);
			}
			CreateFile(successfulFile);
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

		private static void CreateFile(string wFile)
		{
			using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			{ }
		}
	}
}
