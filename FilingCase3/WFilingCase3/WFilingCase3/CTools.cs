using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using System.Diagnostics;

namespace Charlotte
{
	public class CTools
	{
		private string cToolsFile
		{
			get
			{
				string file = "CTools.exe";

				if (File.Exists(file) == false)
					file = @"..\..\..\..\Tools\CTools.exe";

				file = FileTools.makeFullPath(file);
				return file;
			}
		}

		private string[] perform(params string[] args)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string argsFile = wd.makePath();
				string redirFile = wd.makePath();

				File.WriteAllLines(argsFile, args, StringTools.ENCODING_SJIS);

				{
					ProcessStartInfo psi = new ProcessStartInfo();

					psi.FileName = cToolsFile;
					psi.Arguments = "//O " + redirFile + " //R " + argsFile; // 注意 -- //O が先！
					psi.CreateNoWindow = true;
					psi.UseShellExecute = false;

					Process.Start(psi).WaitForExit();
				}

				return File.ReadAllLines(redirFile, StringTools.ENCODING_SJIS);
			}
		}

		private string[] getLinesPrefix(string[] src, string prefix)
		{
			List<string> dest = new List<string>();

			foreach (string line in src)
				if (line.StartsWith(prefix))
					dest.Add(line.Substring(prefix.Length));

			return dest.ToArray();
		}

		private const string RET_PREFIX = "*RET=";

		public string toFairFullPath(string path)
		{
			return getLinesPrefix(perform("/F", path), RET_PREFIX)[0];
		}
	}
}
