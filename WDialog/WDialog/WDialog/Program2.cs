using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using Charlotte.Chocomint.Dialogs;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			ChocomintGeneral.OptionalPostShown = f =>
			{
				f.TopMost = true;
			};

			ArgsReader ar = new ArgsReader(Environment.GetCommandLineArgs(), 1);

			while (ar.HasArgs())
			{
				if (ar.ArgIs("String"))
				{
					string title = ar.NextArg();
					string prompt = ar.NextArg();
					string wFile = ar.NextArg();

					string value = InputStringDlgTools.Show(title, prompt);

					if (value == null)
						value = "";

					File.WriteAllText(wFile, value);
					continue;
				}
				throw new Exception("不明なコマンド引数");
			}
		}
	}
}
