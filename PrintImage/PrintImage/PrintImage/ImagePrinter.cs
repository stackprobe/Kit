using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;

namespace PrintImage
{
	public class ImagePrinter
	{
		private List<string> _imageFiles = new List<string>();
		private LTRB _margin = null;
		private string _paperSizeName = null;
		private string _printerName = null;

		public void addImageFile(string file)
		{
			_imageFiles.Add(file);
		}

		public void setMargin(LTRB margin)
		{
			_margin = margin;
		}

		public void setPaperSizeName(string name)
		{
			_paperSizeName = name;
		}

		public string[] getPaperSizeNames()
		{
			List<string> dest = new List<string>();

			using (PrintDocument pd = new PrintDocument())
			{
				foreach (PaperSize ps in pd.PrinterSettings.PaperSizes)
				{
					dest.Add(ps.PaperName);
				}
			}
			return dest.ToArray();
		}

		public void setPrinterName(string name)
		{
			_printerName = name;
		}

		public string[] getPrinterNames()
		{
			List<string> dest = new List<string>();

			foreach (string name in PrinterSettings.InstalledPrinters)
			{
				dest.Add(name);
			}
			return dest.ToArray();
		}

		public void doPrint()
		{
			if (_imageFiles.Count < 1)
				throw new Exception("印刷イメージがありません。");

			_imgfq = new Queue<string>(_imageFiles);

			using (PrintDocument pd = new PrintDocument())
			{
				if (_margin != null)
				{
					// LRTB 注意！
					pd.DefaultPageSettings.Margins = new Margins(
						_margin.l,
						_margin.r,
						_margin.t,
						_margin.b
						);
				}
				if (_paperSizeName != null)
				{
					pd.DefaultPageSettings.PaperSize = getPaperSize(pd, _paperSizeName);
				}
				if (_printerName != null)
				{
					pd.PrinterSettings.PrinterName = _printerName;
				}
				pd.PrintPage += new PrintPageEventHandler(pd_printPage);
				pd.Print();
			}
		}

		private static PaperSize getPaperSize(PrintDocument pd, string name)
		{
			foreach (PaperSize ps in pd.PrinterSettings.PaperSizes)
			{
				if (ps.PaperName == name)
				{
					return ps;
				}
			}
			throw new Exception("不明な用紙サイズ：" + name);
		}

		private Queue<string> _imgfq;

		private void pd_printPage(object sender, PrintPageEventArgs e)
		{
			using (Image img = Image.FromFile(_imgfq.Dequeue()))
			{
				e.Graphics.DrawImage(
					img,
					Utils.adjustInside(img.Width, img.Height, e.MarginBounds)
					);
				e.HasMorePages = 1 <= _imgfq.Count;
			}
		}
	}
}
