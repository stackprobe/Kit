using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileExporter
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
#if DEBUG
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
#endif
		}

		private static void Main2(string[] args)
		{
			switch (args[0].ToUpper())
			{
				case "/E":
					FileExporter(
						args[1],
						args[2],
						args[3]
						);
					break;

				case "/I":
					FileImporter(
						args[1],
						args[2]
						);
					break;

				case "/EI": // テスト用
					FileExporter(
						args[1],
						args[2],
						args[3]
						);
					Console.WriteLine("Press ENTER to continue");
					Console.ReadLine();
					FileImporter(
						args[4],
						args[5]
						);
					break;

				default:
					throw new Exception("不明なオプション");
			}
		}

		private static void FileExporter(string rDir, string wDir, string successfulFile)
		{
			rDir = Path.GetFullPath(rDir);
			wDir = Path.GetFullPath(wDir);
			successfulFile = Path.GetFullPath(successfulFile);

			Console.WriteLine("rDir: " + rDir);
			Console.WriteLine("wDir: " + wDir);
			Console.WriteLine("successfulFile: " + successfulFile);

			if (Directory.Exists(rDir) == false)
				throw new Exception("rDir not exists");

			if (Directory.Exists(wDir) == false)
				throw new Exception("wDir not exists");

			// successfulFile

			try
			{
				int fCount = 1;

				foreach (string rFile in Directory.GetFiles(rDir, "*", SearchOption.AllDirectories))
				{
					string wFile = Path.Combine(wDir, CTools.ZPad("" + fCount, 5) + ".dat");
					string fFile = Path.Combine(wDir, CTools.ZPad("" + fCount, 5) + ".file");

					Console.WriteLine("< " + rFile);
					Console.WriteLine("> " + wFile);
					Console.WriteLine("f " + fFile);

					File.WriteAllText(fFile, rFile, Encoding.UTF8);

					if (File.Exists(rFile) == false)
						throw new Exception("Before move, rFile not exists: " + rFile);

					if (CTools.Exists(wFile))
						throw new Exception("Before move, wFile exists: " + wFile);

					File.Move(rFile, wFile);

					if (CTools.Exists(rFile))
						throw new Exception("After move, rFile exists: " + rFile);

					if (File.Exists(wFile) == false)
						throw new Exception("After move, wFile not exists: " + wFile);

					fCount++;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				for (int fCount = 1; ; fCount++)
				{
					string rFile = Path.Combine(wDir, CTools.ZPad("" + fCount, 5) + ".dat"); // from wDir !!!
					string fFile = Path.Combine(wDir, CTools.ZPad("" + fCount, 5) + ".file");

					Console.WriteLine("r.< " + rFile);
					Console.WriteLine("r.f " + fFile);

					if (File.Exists(rFile) == false && File.Exists(fFile) == false)
						break;

					try
					{
						string wFile = File.ReadAllText(fFile);

						Console.WriteLine("r.> " + wFile);

						File.Move(rFile, wFile);
						File.Delete(fFile);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}
				}
				throw new Exception("エラーによりリストアしました。", e);
			}
			CTools.CreateFile(successfulFile);
			Console.WriteLine("done!");
		}

		private static void FileImporter(string rDir, string successfulFile)
		{
			rDir = Path.GetFullPath(rDir);
			successfulFile = Path.GetFullPath(successfulFile);

			Console.WriteLine("rDir: " + rDir);
			Console.WriteLine("successfulFile: " + successfulFile);

			if (Directory.Exists(rDir) == false)
				throw new Exception("rDir not exists");

			// successfulFile

			foreach (string file in Directory.GetFiles(rDir))
			{
				if (Path.GetExtension(file).ToLower() == ".dat")
				{
					string rFile = file;
					string fFile = CTools.ChangeSuffix(file, ".dat", ".file");

					Console.WriteLine("< " + rFile);
					Console.WriteLine("f " + fFile);

					string wFile = File.ReadAllText(fFile);

					Console.WriteLine("> " + wFile);

					if (File.Exists(rFile) == false)
						throw new Exception("Before move, rFile not exists: " + rFile);

					if (CTools.Exists(wFile))
						throw new Exception("Before move, wFile exists: " + wFile);

					{
						string wDir = Path.GetDirectoryName(wFile);

						if (Directory.Exists(wDir) == false)
							Directory.CreateDirectory(wDir);
					}

					File.Move(rFile, wFile);

					if (CTools.Exists(rFile))
						throw new Exception("After move, rFile exists: " + rFile);

					if (File.Exists(wFile) == false)
						throw new Exception("After move, wFile not exists: " + wFile);

					File.Delete(fFile);
				}
			}
			CTools.CreateFile(successfulFile);
			Console.WriteLine("done!");
		}
	}
}
