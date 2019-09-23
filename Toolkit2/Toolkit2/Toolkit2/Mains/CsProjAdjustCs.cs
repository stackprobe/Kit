using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Mains
{
	public class CsProjAdjustCs
	{
		public string RootDir;
		public string ProjectLocalFile;
		public string CsRelDir;

		// <---- prm

		private const string CS_LINE_FILE_PREFIX = "    <Compile Include=\"";
		private const string CS_LINE_FILE_SUFFIX = "\" />";

		private string ProjectFile;
		private string CsDir;

		private bool IsTargetCsLine(string line)
		{
			return
				line.StartsWith(CS_LINE_FILE_PREFIX + this.CsRelDir + "\\") &&
				line.EndsWith(CS_LINE_FILE_SUFFIX);
		}

		private bool IsCsLine(string line)
		{
			return
				line.StartsWith(CS_LINE_FILE_PREFIX) &&
				line.EndsWith(CS_LINE_FILE_SUFFIX);
		}

		public void Perform()
		{
			this.RootDir = FileTools.MakeFullPath(this.RootDir);

			if (this.ProjectLocalFile == "")
				throw new Exception("Bad ProjectLocalFile");

			if (this.CsRelDir == "")
				throw new Exception("Bad CsRelDir");

			this.ProjectFile = Path.Combine(this.RootDir, this.ProjectLocalFile);
			this.CsDir = Path.Combine(this.RootDir, this.CsRelDir);

			if (Directory.Exists(this.RootDir) == false)
				throw new Exception("no RootDir");

			if (File.Exists(this.ProjectFile) == false)
				throw new Exception("no ProjectFile");

			FileTools.CreateDir(this.CsDir);

			List<string> lines = File.ReadAllLines(this.ProjectFile, Encoding.UTF8).ToList();

			string[] csFiles = Directory.GetFiles(this.CsDir, "*", SearchOption.AllDirectories)
				.Where(file => Path.GetExtension(file).ToLower() == ".cs")
				.Select(file => FileTools.ChangeRoot(file, this.CsDir))
				.ToArray();

			Array.Sort(csFiles, (a, b) => StringTools.CompIgnoreCase(a, b));

			int targetCsIndex = ArrayTools.IndexOf(lines.ToArray(), line => this.IsTargetCsLine(line));

			if (targetCsIndex == -1)
			{
				targetCsIndex = ArrayTools.LastIndexOf(lines.ToArray(), line => this.IsCsLine(line));

				if (targetCsIndex == -1)
					throw new Exception("Bad targetCsIndex");

				targetCsIndex++;
			}

			lines = lines
				.Where(line => this.IsTargetCsLine(line) == false)
				.ToList();

			lines.InsertRange(targetCsIndex, csFiles.Select(file => CS_LINE_FILE_PREFIX + this.CsRelDir + "\\" + file + CS_LINE_FILE_SUFFIX));

			File.WriteAllLines(this.ProjectFile, lines, Encoding.UTF8);

			// 終端の改行を除去
			{
				string text = File.ReadAllText(this.ProjectFile, Encoding.UTF8);

				text = text.TrimEnd("\r\n".ToArray());

				File.WriteAllText(this.ProjectFile, text, Encoding.UTF8);
			}
		}
	}
}
