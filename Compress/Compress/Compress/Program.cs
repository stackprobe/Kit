using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Compress
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void Main2(string[] args)
		{
			string mode = args[0];
			string rFile = args[1];
			string wFile = args[2];

			try
			{
				switch (mode.ToUpper())
				{
					case "/C":
						using (FileStream rfs = new FileStream(rFile, FileMode.Open, FileAccess.Read))
						using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
						using (GZipStream gzs = new GZipStream(wfs, CompressionMode.Compress))
						{
							//CopyTo(rfs, gzs); // あんま変わらん..
							rfs.CopyTo(gzs);
						}
						break;

					case "/D":
						using (FileStream rfs = new FileStream(rFile, FileMode.Open, FileAccess.Read))
						using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
						using (GZipStream gzs = new GZipStream(rfs, CompressionMode.Decompress))
						{
							//CopyTo(gzs, wfs); // あんま変わらん..
							gzs.CopyTo(wfs);
						}
						break;

					default:
						throw new Exception("不明なオプション");
				}
			}
			catch (Exception e)
			{
				File.Delete(wFile);
				throw e;
			}
		}

#if false
		private const int BUFF_SIZE = 128 * 1024 * 1024;

		private static void CopyTo(Stream rs, Stream ws)
		{
			byte[] buff = new byte[BUFF_SIZE];

			for (; ; )
			{
				int readSize = rs.Read(buff, 0, BUFF_SIZE);

				if (readSize <= 0)
					break;

				ws.Write(buff, 0, readSize);
			}
		}
#endif
	}
}
