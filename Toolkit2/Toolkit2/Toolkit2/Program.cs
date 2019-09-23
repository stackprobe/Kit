using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Mains;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{919b3759-6a55-4c3d-b32d-46005e8d3861}";
		public const string APP_TITLE = "Toolkit2";

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

		private void Main2(ArgsReader ar)
		{
			if (ar.ArgIs("/CS-PROJ-ADJUST-CS"))
			{
				string rootDir = ar.NextArg();
				string projectLocalFile = ar.NextArg();
				string csRelDir = ar.NextArg();
				string successfulFile = ar.NextArg();

				new CsProjAdjustCs()
				{
					RootDir = rootDir,
					ProjectLocalFile = projectLocalFile,
					CsRelDir = csRelDir,
				}
				.Perform();

				File.WriteAllBytes(successfulFile, BinTools.EMPTY);
			}
		}
	}
}
