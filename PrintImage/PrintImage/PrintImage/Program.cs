using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PrintImage
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (Utils.equalsIgnoreCase(args[0], "//R"))
				{
					args = File.ReadAllLines(args[1], Utils.ENCODING_SJIS);
				}
				main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static void main2(string[] args)
		{
			Queue<string> argq = new Queue<string>(args);

			ImagePrinter imgPrt = new ImagePrinter();

			while (1 <= argq.Count)
			{
				string arg = argq.Dequeue();

				if (Utils.equalsIgnoreCase(arg, "/F"))
				{
					imgPrt.addImageFile(argq.Dequeue());
					continue;
				}
				if (Utils.equalsIgnoreCase(arg, "/M"))
				{
					int l = int.Parse(argq.Dequeue());
					int t = int.Parse(argq.Dequeue());
					int r = int.Parse(argq.Dequeue());
					int b = int.Parse(argq.Dequeue());

					imgPrt.setMargin(new LTRB(l, t, r, b));
					continue;
				}
				if (Utils.equalsIgnoreCase(arg, "/SL"))
				{
					Console.WriteLine("*PAPER_SIZES_BEGIN");

					foreach (string name in imgPrt.getPaperSizeNames())
					{
						Console.WriteLine("*PAPER_SIZE=" + name);
					}
					Console.WriteLine("*PAPER_SIZES_END");
					return;
				}
				if (Utils.equalsIgnoreCase(arg, "/S"))
				{
					imgPrt.setPaperSizeName(argq.Dequeue());
					continue;
				}
				if (Utils.equalsIgnoreCase(arg, "/PL"))
				{
					Console.WriteLine("*PRINTERS_BEGIN");

					foreach (string name in imgPrt.getPrinterNames())
					{
						Console.WriteLine("*PRINTER=" + name);
					}
					Console.WriteLine("*PRINTERS_END");
					return;
				}
				if (Utils.equalsIgnoreCase(arg, "/P"))
				{
					imgPrt.setPrinterName(argq.Dequeue());
					continue;
				}
				throw new Exception("不明な引数：" + arg);
			}
			imgPrt.doPrint();
		}
	}
}
