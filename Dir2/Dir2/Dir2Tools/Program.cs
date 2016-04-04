using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Dir2Tools
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
			Main3(args[0], args[1], args[2]);
		}

		private static void Main3(string command, string path, string successfulFile)
		{
			command = command.ToUpper();

			switch (command)
			{
				case "/MD":
					Directory.CreateDirectory(path);
					break;

				case "/RD":
					Directory.Delete(path, true);
					break;

				case "/DEL":
					File.Delete(path);
					break;

				default:
					throw new Exception("不明なコマンド_M3: " + command);
			}
			CreateFile(successfulFile);
		}

		private static void CreateFile(string wFile)
		{
			using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			{ }
		}
	}
}
