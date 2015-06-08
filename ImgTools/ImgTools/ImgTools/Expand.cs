using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTools
{
	public class Expand
	{
		private ImageData _src;
		private ImageData _dest;

		public ImageData Main(ImageData src, int new_w, int new_h)
		{
			_src = src;
			_dest = new ImageData(new_w, new_h);

			this.Main2();

			return _dest;
		}

		private List<LineInfo> VLines;
		private List<LineInfo> HLines;

		private class LineInfo
		{
			public List<RangeInfo> RangeList = new List<RangeInfo>();
		}

		private class RangeInfo
		{
			public int DPos;
			public int SPos;
			public int E_Bgn;
			public int E_End;
			public int Count;
		}

		private List<LineInfo> MakeLines(int src_w, int dest_w)
		{
			List<LineInfo> lines = new List<LineInfo>();

			for (int dPos = 0; dPos < dest_w; dPos++)
			{
				int eBgn = dPos * src_w;
				int eEnd = (dPos + 1) * src_w - 1;

				int sBgn = eBgn / dest_w;
				int sEnd = eEnd / dest_w;

				LineInfo li = new LineInfo();

				for (int sPos = sBgn; sPos <= sEnd; sPos++)
				{
					RangeInfo ri = new RangeInfo();

					ri.DPos = dPos;
					ri.SPos = sPos;

					if (sPos == sBgn)
						ri.E_Bgn = eBgn;
					else
						ri.E_Bgn = sPos * dest_w;

					if (sPos == sEnd)
						ri.E_End = eEnd;
					else
						ri.E_End = (sPos + 1) * dest_w - 1;

					ri.Count = ri.E_End + 1 - ri.E_Bgn;

					li.RangeList.Add(ri);
				}
				lines.Add(li);
			}
			return lines;
		}

		private void Main2()
		{
			this.VLines = this.MakeLines(_src.Get_W(), _dest.Get_W());
			this.HLines = this.MakeLines(_src.Get_H(), _dest.Get_H());

			Console.WriteLine("[V-Lines]");
			this.DebugPrintLines(this.VLines);
			Console.WriteLine("[H-Lines]");
			this.DebugPrintLines(this.HLines);

			for (int color = 0; color < 4; color++)
			{
				for (int x = 0; x < _dest.Get_W(); x++)
				{
					for (int y = 0; y < _dest.Get_H(); y++)
					{
						LineInfo vli = this.VLines[x];
						LineInfo hli = this.HLines[y];

						long numer = 0;
						long denom = 0;

						for (int v = 0; v < vli.RangeList.Count; v++)
						{
							for (int h = 0; h < hli.RangeList.Count; h++)
							{
								RangeInfo vri = vli.RangeList[v];
								RangeInfo hri = hli.RangeList[h];

								long weight = vri.Count * hri.Count;

								if (color != DotData.A)
									weight *= _src.GetDot(vri.SPos, hri.SPos).C[DotData.A];

								numer += weight * _src.GetDot(vri.SPos, hri.SPos).C[color];
								denom += weight;
							}
						}
						int cVal;

						if (denom == 0)
							cVal = 0;
						else
							cVal = (int)IntTools.DivRndOff(numer, denom);

						_dest.GetDot(x, y).C[color] = cVal;
					}
				}
			}
		}

		private void DebugPrintLines(List<LineInfo> lines)
		{
			int c = 0;

			foreach (LineInfo li in lines)
			{
				foreach (RangeInfo ri in li.RangeList)
				{
					Console.WriteLine(ri.DPos + " <- " + ri.SPos + " * " + ri.Count);
					c++;

					if (20 <= c)
					{
						Console.WriteLine("...");
						return;
					}
				}
			}
		}
	}
}
