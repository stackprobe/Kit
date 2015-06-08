using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTools
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private string _rImgFile;
		private string _wImgFile;

		private ImageData _img;
		private ImageData _img2;

		private void Main2(string[] args)
		{
			ArgsReader ar = new ArgsReader(args);

			for (; ; )
			{
				if (ar.ArgIs("/F"))
				{
					string file = ar.NextArg();
					_rImgFile = file;
					_wImgFile = FileTools.UpdateSuffixLoop(file, ".png");
					continue;
				}
				if (ar.ArgIs("/RWF"))
				{
					string file = ar.NextArg();
					_rImgFile = file;
					_wImgFile = file;
					continue;
				}
				if (ar.ArgIs("/RF"))
				{
					_rImgFile = ar.NextArg();
					continue;
				}
				if (ar.ArgIs("/WF"))
				{
					_wImgFile = ar.NextArg();
					continue;
				}
				break;
			}

			_img = new ImageData();

			if (_rImgFile != null)
				_img.Load(_rImgFile);

			if (_wImgFile == null)
				throw new Exception("出力ファイルが指定されていません。");

			// 上書きのとき怖い
			//FileTools.WriteFileTest(_wImgFile);
			//FileTools.DeleteFile(_wImgFile);

			for (; ; )
			{
				if (ar.ArgIs("/E"))
				{
					int new_w = int.Parse(ar.NextArg());
					int new_h = int.Parse(ar.NextArg());

					_img = new Expand().Main(_img, new_w, new_h);

					continue;
				}
				if (ar.ArgIs("/C"))
				{
					int l = int.Parse(ar.NextArg());
					int t = int.Parse(ar.NextArg());
					int w = int.Parse(ar.NextArg());
					int h = int.Parse(ar.NextArg());

					_img = Conv.Cut(_img, l, t, w, h);

					continue;
				}
				if (ar.ArgIs("/TW")) // 右上と左下を反転
				{
					_img = Conv.Twist(_img);

					continue;
				}
				if (ar.ArgIs("/T")) // 上下反転
				{
					_img = Conv.Turn(_img);

					continue;
				}
				if (ar.ArgIs("/R")) // 回転
				{
					int rot = int.Parse(ar.NextArg());

					_img = Conv.Rotate(_img, rot);

					continue;
				}
				if (ar.ArgIs("/TLR")) // 左右反転
				{
					_img = Conv.LRTurn(_img);

					continue;
				}
				if (ar.ArgIs("/M"))
				{
					int dir = int.Parse(ar.NextArg());

					_img = Conv.Mirror(_img, dir);

					continue;
				}
				if (ar.ArgIs("/TP"))
				{
					int trans_x = int.Parse(ar.NextArg());
					int trans_y = int.Parse(ar.NextArg());

					Conv.TransPoint(_img, trans_x, trans_y);

					continue;
				}
				if (ar.ArgIs("/TC"))
				{
					int trans_r = int.Parse(ar.NextArg());
					int trans_g = int.Parse(ar.NextArg());
					int trans_b = int.Parse(ar.NextArg());

					Conv.TransColor(_img, trans_r, trans_g, trans_b);

					continue;
				}
				if (ar.ArgIs("/ROT")) // ラジアン角を指定して回転
				{
					double angle = double.Parse(ar.NextArg());
					double centralX = _img.Get_W() / 2.0;
					double centralY = _img.Get_H() / 2.0;
					DotData dummyDot = Consts.DummyDot;
					int dotDiv = 16;

					_img = Conv.Rotate(_img, angle, centralX, centralY, dummyDot, dotDiv);

					continue;
				}
				if (ar.ArgIs("/ROTEX")) // ラジアン角と「回転の中心」と「領域外の色」と「精度」を指定して回転
				{
					double angle = double.Parse(ar.NextArg());
					double centralX = double.Parse(ar.NextArg());
					double centralY = double.Parse(ar.NextArg());
					int dummy_a = int.Parse(ar.NextArg());
					int dummy_r = int.Parse(ar.NextArg());
					int dummy_g = int.Parse(ar.NextArg());
					int dummy_b = int.Parse(ar.NextArg());
					int dotDiv = int.Parse(ar.NextArg()); // 1～

					DotData dummyDot = new DotData(dummy_a, dummy_r, dummy_g, dummy_b);

					_img = Conv.Rotate(_img, angle, centralX, centralY, dummyDot, dotDiv);

					continue;
				}
				if (ar.ArgIs("/EXTEND"))
				{
					int l = int.Parse(ar.NextArg());
					int t = int.Parse(ar.NextArg());
					int r = int.Parse(ar.NextArg());
					int b = int.Parse(ar.NextArg());
					DotData dummyDot = Consts.DummyDot;

					_img = Conv.Extend(_img, l, t, r, b, dummyDot);

					continue;
				}
				if (ar.ArgIs("/EXTENDEX"))
				{
					int l = int.Parse(ar.NextArg());
					int t = int.Parse(ar.NextArg());
					int r = int.Parse(ar.NextArg());
					int b = int.Parse(ar.NextArg());
					int dummy_a = int.Parse(ar.NextArg());
					int dummy_r = int.Parse(ar.NextArg());
					int dummy_g = int.Parse(ar.NextArg());
					int dummy_b = int.Parse(ar.NextArg());

					DotData dummyDot = new DotData(dummy_a, dummy_r, dummy_g, dummy_b);

					_img = Conv.Extend(_img, l, t, r, b, dummyDot);

					continue;
				}
				if (ar.ArgIs("/PLAIN"))
				{
					int w = int.Parse(ar.NextArg());
					int h = int.Parse(ar.NextArg());
					DotData dummyDot = Consts.DummyDot;

					_img = new ImageData(w, h);
					_img.SetDotAll(dummyDot);

					continue;
				}
				if (ar.ArgIs("/PLAINEX"))
				{
					int w = int.Parse(ar.NextArg());
					int h = int.Parse(ar.NextArg());
					int dummy_a = int.Parse(ar.NextArg());
					int dummy_r = int.Parse(ar.NextArg());
					int dummy_g = int.Parse(ar.NextArg());
					int dummy_b = int.Parse(ar.NextArg());

					DotData dummyDot = new DotData(dummy_a, dummy_r, dummy_g, dummy_b);

					_img = new ImageData(w, h);
					_img.SetDotAll(dummyDot);

					continue;
				}
				if (ar.ArgIs("/2"))
				{
					string img2file = ar.NextArg();

					_img2 = new ImageData();
					_img2.Load(img2file);

					continue;
				}
				if (ar.ArgIs("/2X"))
				{
					ImageData tmp = _img;
					_img = _img2;
					_img2 = tmp;

					continue;
				}
				if (ar.ArgIs("/PASTE"))
				{
					int l = int.Parse(ar.NextArg());
					int t = int.Parse(ar.NextArg());

					Conv.Paste(_img, l, t, _img2);

					continue;
				}
				if (ar.ArgIs("/PRTSC"))
				{
					_img.Load(WinTools.PrintScreen());
				}
				break;
			}

			if (ar.HasArgs(1))
				throw new Exception("不明なコマンド引数: " + ar.NextArg());

			_img.Save(_wImgFile);
		}
	}
}
