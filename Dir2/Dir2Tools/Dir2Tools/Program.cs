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
				Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static void Main2(string[] args)
		{
			switch (args.Length)
			{
				case 3:
					Main3(args[0], args[1], args[2]);
					break;

				case 4:
					Main4(args[0], args[1], args[2], args[3]);
					break;

				default:
					throw new Exception("不正なコマンド引数");
			}
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

		private static void Main4(string command, string rFile, string wFile, string successfulFile)
		{
			command = command.ToUpper();

			switch (command)
			{
				case "/MOVE":
					File.Move(rFile, wFile);
					break;

				default:
					throw new Exception("不明なコマンド_M4: " + command);
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
