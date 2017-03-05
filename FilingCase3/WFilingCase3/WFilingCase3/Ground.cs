using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

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

		public string dummyConfString = "default";
		public bool dummyConfFlag = false;
		public int dummyConfInt = -1;

		public void loadConf()
		{
			try
			{
				List<string> lines = new List<string>();

				foreach (string line in FileTools.readAllLines(getConfFile(), StringTools.ENCODING_SJIS))
					if (line != "" && line[0] != ';')
						lines.Add(line);

				int c = 0;

				// items >

				dummyConfString = lines[c++];
				dummyConfFlag = StringTools.toFlag(lines[c++]);
				dummyConfInt = IntTools.toInt(lines[c++]);

				// < items
			}
			catch
			{ }
		}

		private string getConfFile()
		{
			return Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".conf");
		}

		// ---- saved data ----

		public string dummyDataString = "default";
		public bool dummyDataFlag = false;
		public int dummyDataInt = -1;

		public void loadData()
		{
			try
			{
				string[] lines = File.ReadAllLines(getDataFile(), Encoding.UTF8);
				int c = 0;

				// items >

				dummyDataString = lines[c++];
				dummyDataFlag = StringTools.toFlag(lines[c++]);
				dummyDataInt = IntTools.toInt(lines[c++]);

				// < items
			}
			catch
			{ }
		}

		public void saveData()
		{
			try
			{
				List<string> lines = new List<string>();

				// items >

				lines.Add(dummyDataString);
				lines.Add(StringTools.toString(dummyDataFlag));
				lines.Add("" + dummyDataInt);

				// < items

				File.WriteAllLines(getDataFile(), lines, Encoding.UTF8);
			}
			catch
			{ }
		}

		private string getDataFile()
		{
			return Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".dat");
		}

		// ----
	}
}
