﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Charlotte
{
	public class Gnd
	{
		private static Gnd _i = null;

		public static Gnd i
		{
			get
			{
				if (_i == null)
					_i = new Gnd();

				return _i;
			}
		}

		private Gnd()
		{ }

		// ---- conf data ----

		public Consts.ShowConsole_e showConsole = Consts.ShowConsole_e.HIDE;

		public void loadConf()
		{
			try
			{
				List<string> lines = new List<string>();

				foreach (string line in FileTools.readAllLines(getConfFile(), StringTools.ENCODING_SJIS))
					if (line != "" && line[0] != ';')
						lines.Add(line);

				int c = 0;

				if (lines.Count != int.Parse(lines[c++]))
					throw new Exception();

				// items >

				showConsole = (Consts.ShowConsole_e)int.Parse(lines[c++]);

				// < items
			}
			catch (Exception e)
			{
				MessageBox.Show("" + e, "Error @ loadConf()");
			}
		}

		private string getConfFile()
		{
			string file = Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".conf");

			if (File.Exists(file) == false)
			{
				file = @"..\..\..\..\doc\WFilingCase3.conf"; // @ devenv

				if (File.Exists(file) == false)
					throw new Exception("no .conf");
			}
			return file;
		}

		// ---- saved data ----

		public int portNo = 65123;
		public int connectMax = 100;
		public string rootDir = defRootDir;
		public int keepDiskFree_MB = 500;

		public void loadData()
		{
			try
			{
				if (File.Exists(getDataFile()) == false)
					return;

				string[] lines = File.ReadAllLines(getDataFile(), Encoding.UTF8);
				int c = 0;

				// items >

				portNo = IntTools.toInt(lines[c++], 1, 65535);
				connectMax = IntTools.toInt(lines[c++], 1, IntTools.IMAX);
				rootDir = lines[c++];
				keepDiskFree_MB = IntTools.toInt(lines[c++]);

				// < items
			}
			catch (Exception e)
			{
				MessageBox.Show("" + e, "Error @ loadData()");
			}
		}

		public void saveData()
		{
			try
			{
				List<string> lines = new List<string>();

				// items >

				lines.Add("" + portNo);
				lines.Add("" + connectMax);
				lines.Add(rootDir);
				lines.Add("" + keepDiskFree_MB);

				// < items

				File.WriteAllLines(getDataFile(), lines, Encoding.UTF8);
			}
			catch (Exception e)
			{
				MessageBox.Show("" + e, "Error @ saveData()");
			}
		}

		private string getDataFile()
		{
			return Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".dat");
		}

		// ----

		public static string defRootDir
		{
			get
			{
				return Path.Combine(FileTools.getProgramData(), @"cerulean charlotte\FilingCase3");
			}
		}

		public string getIconFile(string name)
		{
			string file = name + ".dat";

			if (File.Exists(file) == false)
				file = @"..\..\..\..\res\" + name + ".ico";

			file = FileTools.makeFullPath(file);
			return file;
		}

		public CTools cTools = new CTools();
		public Icon iconServerRunning;
		public Icon iconServerNotRunning;
		public ServerProc serverProc = new ServerProc();
		public NamedEventObject evStop;
	}
}
