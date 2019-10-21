using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Text.RegularExpressions;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{27d946c9-0067-422a-89da-5f4e7bc482e7}";
		public const string APP_TITLE = "LinarCacheTrim";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private string CacheRootDir = @"C:\tmp\LinarCache";

		private class DriveManagerInfo
		{
			private class Info
			{
				/// <summary>
				/// ドライブ文字
				/// 大文字で格納
				/// 比較：大文字・小文字を区別しない。
				/// </summary>
				public char Drive;

				/// <summary>
				/// ボリュームシリアル番号
				/// 大文字で格納
				/// 比較：大文字・小文字を区別しない。
				/// </summary>
				public string VolumeSerial;
			}

			private Info[] Drives;

			private static string GetVolumeSerial(char drive)
			{
				using (WorkingDir wd = new WorkingDir())
				{
					string outFile = wd.MakePath();

					ProcessTools.Batch(new string[] { string.Format("> \"{0}\" DIR {1}:\\Dummy", outFile, drive) });

					return File.ReadAllLines(outFile, StringTools.ENCODING_SJIS)
						.Where(line => Regex.IsMatch(line, "^ ボリューム シリアル番号は [0-9A-F]{4}-[0-9A-F]{4} です$"))
						.Select(line => line.Substring(15, 4) + line.Substring(20, 4))
						.First();
				}
			}

			public DriveManagerInfo()
			{
				this.Drives = DriveInfo.GetDrives().Where(v => v.IsReady).Select(v => char.ToUpper(v.Name[0])).Select(drive => new Info()
				{
					Drive = drive,
					VolumeSerial = GetVolumeSerial(drive),
				})
				.ToArray();
			}

			public string DriveToVolumeSerial(char drive)
			{
				Info info = this.Drives.Where(i => i.Drive == char.ToUpper(drive)).FirstOrDefault();

				if (info == null)
					return "XXXX-XXXX"; // dummy value

				return info.VolumeSerial;
			}

			public char VolumeSerialToDrive(string volumeSerial)
			{
				Info info = this.Drives.Where(i => i.VolumeSerial == volumeSerial.ToUpper()).FirstOrDefault();

				if (info == null)
					return 'X'; // dummy value

				return info.Drive;
			}
		}

		private class CachedDirInfo
		{
			public string TargetDir;
			public string CacheLocalFile;
		}

		private CachedDirInfo[] LoadCacheListFile(string file)
		{
			string[] lines = File.ReadAllLines(file, StringTools.ENCODING_SJIS);
			int index = 0;

			if (lines[index++] != "[Thumbsnail]")
				throw new Exception("CacheListFile L-1 broken");

			if (Regex.IsMatch(lines[index++], "^CacheSeqNo=[0-9]+$") == false)
				throw new Exception("CacheListFile L-2 broken");

			List<CachedDirInfo> ret = new List<CachedDirInfo>();

			while (index < lines.Length)
			{
				string line = lines[index++];

				if (Regex.IsMatch(line, "^[0-9A-F]{8}\\\\.+=.+$") == false)
					throw new Exception("CacheListFile L-x broken");

				CachedDirInfo cachedDir = new CachedDirInfo();

				{
					int p = line.LastIndexOf('=');

					if (p == -1)
						throw null; // never

					cachedDir.TargetDir = line.Substring(0, p);
					cachedDir.CacheLocalFile = line.Substring(p + 1);
				}

				cachedDir.TargetDir = DriveManager.VolumeSerialToDrive(cachedDir.TargetDir.Substring(0, 8)) + ":" + cachedDir.TargetDir.Substring(8);

				ret.Add(cachedDir);
			}
			return ret.ToArray();
		}

		private DriveManagerInfo DriveManager;
		private string CacheListFile;
		private CachedDirInfo[] CachedDirs;

		private void Main2(ArgsReader ar)
		{
			DriveManager = new DriveManagerInfo();

		readArgs:
			if (ar.ArgIs("/D"))
			{
				CacheRootDir = ar.NextArg();
				goto readArgs;
			}
			if (ar.HasArgs())
				throw new Exception("不明なコマンド引数");

			// ---- コマンド引数の読み込み ...

			CacheRootDir = FileTools.MakeFullPath(CacheRootDir);

			if (Directory.Exists(CacheRootDir) == false)
				throw new Exception("no CacheRootDir");

			CacheListFile = Path.Combine(CacheRootDir, "_cache_.lst");

			if (File.Exists(CacheListFile) == false)
				throw new Exception("no CacheListFile");

			CachedDirs = LoadCacheListFile(CacheListFile);

			// ---- 処理 ...

			for (int index = 0; index < CachedDirs.Length; index++)
			{
				CachedDirInfo cachedDir = CachedDirs[index];

				// ? 削除対象
				if (
					Regex.IsMatch(cachedDir.TargetDir, "^[A-Z]:\\\\[0-9]{1,3}\\\\.+$", RegexOptions.IgnoreCase) ||
					StringTools.CompIgnoreCase(cachedDir.TargetDir, "C:\\temp") == 0 ||
					Directory.Exists(cachedDir.TargetDir) == false
					)
				{
					string cacheFile = Path.Combine(CacheRootDir, cachedDir.CacheLocalFile);

					Console.WriteLine("DELETE " + cacheFile); // test

					FileTools.Delete(cacheFile);
					CachedDirs[index] = null;
				}
			}
			CachedDirs = CachedDirs.Where(v => v != null).ToArray();
			Array.Sort(CachedDirs, (a, b) => StringTools.CompIgnoreCase(a.TargetDir, b.TargetDir));
			RenumberCachedDirs();
			SaveCacheListFile();
		}

		private void RenumberCachedDirs()
		{
			List<string[]> renamePairs1 = new List<string[]>();
			List<string[]> renamePairs2 = new List<string[]>();

			for (int index = 0; index < CachedDirs.Length; index++)
			{
				CachedDirInfo cachedDir = CachedDirs[index];
				string cacheLocalFileNew = string.Format("{0:D8}.dvt", index);

				if (StringTools.CompIgnoreCase(cachedDir.CacheLocalFile, cacheLocalFileNew) != 0)
				{
					string rCacheFile = Path.Combine(CacheRootDir, cachedDir.CacheLocalFile);
					string midFile = Path.Combine(CacheRootDir, string.Format("{0}_{1}.tmp", cacheLocalFileNew, Guid.NewGuid().ToString("B")));
					string wCacheFile = Path.Combine(CacheRootDir, cacheLocalFileNew);

					Console.WriteLine("< " + rCacheFile); // test
					Console.WriteLine("* " + midFile); // test
					Console.WriteLine("> " + wCacheFile); // test

					cachedDir.CacheLocalFile = cacheLocalFileNew;

					renamePairs1.Add(new string[] { rCacheFile, midFile });
					renamePairs2.Add(new string[] { midFile, wCacheFile });
				}
			}
			foreach (string[] renamePair in EnumerableTools.Linearize(renamePairs1, renamePairs2))
			{
				string rFile = renamePair[0];
				string wFile = renamePair[1];

				Console.WriteLine("< " + rFile); // test
				Console.WriteLine("> " + wFile); // test

				File.Move(rFile, wFile);
			}
		}

		private void SaveCacheListFile()
		{
			File.WriteAllLines(CacheListFile, GetCachedListFileLines(), StringTools.ENCODING_SJIS);
		}

		private IEnumerable<string> GetCachedListFileLines()
		{
			yield return "[Thumbsnail]";
			yield return string.Format("CacheSeqNo={0}", CachedDirs.Length);

			foreach (CachedDirInfo cachedDir in CachedDirs)
			{
				yield return string.Format("{0}{1}={2}",
					DriveManager.DriveToVolumeSerial(cachedDir.TargetDir[0]),
					cachedDir.TargetDir.Substring(2),
					cachedDir.CacheLocalFile
					);
			}
		}
	}
}
