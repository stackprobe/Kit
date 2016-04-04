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
			AddFilePart(args[0], args[1], long.Parse(args[2]), args[3]);
		}

		private static void AddFilePart(string rFile, string wFile, long startPos, string successfulFile)
		{
			if (File.Exists(wFile) == false)
				CreateFile(wFile);

			long rSize = new FileInfo(rFile).Length;
			long wSize = new FileInfo(wFile).Length;

			if (startPos < 0 || wSize < startPos)
				throw new Exception("書き込み開始位置エラー: " + startPos + ", " + wSize);

			using (FileStream rfs = new FileStream(rFile, FileMode.Open, FileAccess.Read))
			using (FileStream wfs = new FileStream(wFile, FileMode.Open, FileAccess.Write)) // don't FileMode.Create !!!
			{
				wfs.Seek(startPos, SeekOrigin.Begin);
				ReadWriteStream(rfs, wfs, rSize);
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
