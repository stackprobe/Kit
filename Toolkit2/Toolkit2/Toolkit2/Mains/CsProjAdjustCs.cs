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

		private string ProjectFile;
		private string CsDir;

		private class LineInfo
		{
			public string Line;

			public bool IsTargetCs(string csRelDir)
			{
				return
					this.Line.StartsWith("    <Compile Include=\"" + csRelDir + "\\") &&
					this.Line.EndsWith("\" />");
			}
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

			if (Directory.Exists(this.CsDir) == false)
				throw new Exception("no CsDir");

			List<LineInfo> lines = File.ReadAllLines(this.ProjectFile, Encoding.UTF8).Select(line => new LineInfo()
			{
				Line = line,
			})
			.ToList();

			string[] csFiles = Directory.GetFiles(this.CsDir, "*", SearchOption.AllDirectories)
				.Where(file => Path.GetExtension(file).ToLower() == ".cs")
				.Select(file => FileTools.ChangeRoot(file, this.CsDir))
				.ToArray();

			Array.Sort(csFiles, (a, b) => StringTools.CompIgnoreCase(a, b));

			int targetCsIndex = ArrayTools.IndexOf(lines.ToArray(), line => line.IsTargetCs(this.CsRelDir));

			if (targetCsIndex == -1)
				throw new Exception("Bad targetCsIndex");

			lines = lines
				.Where(line => line.IsTargetCs(this.CsRelDir) == false)
				.ToList();

			lines.InsertRange(targetCsIndex, csFiles.Select(file => new LineInfo()
			{
				Line = "    <Compile Include=\"" + this.CsRelDir + "\\" + file + "\" />",
			}
			));

			File.WriteAllLines(this.ProjectFile, lines.Select(line => line.Line), Encoding.UTF8);

			// 終端の改行を除去
			{
				string text = File.ReadAllText(this.ProjectFile, Encoding.UTF8);

				text = text.TrimEnd("\r\n".ToArray());

				File.WriteAllText(this.ProjectFile, text, Encoding.UTF8);
			}
		}
	}
}
