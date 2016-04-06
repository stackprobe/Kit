using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace HGet
{
	class Program
	{
		private static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		static void Main(string[] args)
		{
			try
			{
				if (args[0].ToUpper() == "//R")
				{
					Main2(File.ReadAllLines(args[1], ENCODING_SJIS));
				}
				else
				{
					Main2(args);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static bool ArgIs(Queue<string> argq, string spell)
		{
			if (IsSameIgnoreCase(argq.Peek(), spell))
			{
				argq.Dequeue();
				return true;
			}
			return false;
		}

		public enum Method_e
		{
			HEAD,
			GET,
			POST,
		}

		public enum Version_e
		{
			HTTP_10,
			HTTP_11,
		}

		public enum ProxyMode_e
		{
			DIRECT,
			IE,
			SPECIAL,
		}

		private static ProxyMode_e _proxyMode = ProxyMode_e.IE;
		private static string _proxyHost = "localhost";
		private static int _proxyPort = 8080;
		private static int _timeoutMillis = 20000;
		private static Method_e _method = Method_e.GET;
		private static string _url = "http://localhost/";
		private static Version_e _version = Version_e.HTTP_11;
		private static List<string[]> _headerFields = new List<string[]>();
		private static byte[] _body = null; // null == no body
		private static string _successfulFile = @"C:\temp\HGet_successful.flg";
		private static string _resHeaderFieldsFile = @"C:\temp\HGet_resHeaderFields.txt";
		private static string _resBodyFile = @"C:\temp\HGet_resBody.dat";
		private static long _resBodyFileSizeMax = 5000000000L; // 5 GB

		private static void Main2(string[] args)
		{
			Queue<string> argq = new Queue<string>(args);

			while (ArgIs(argq, "//") == false)
			{
				if (ArgIs(argq, "/P"))
				{
					switch (argq.Dequeue().ToUpper())
					{
						case "DIRECT":
							_proxyMode = ProxyMode_e.DIRECT;
							break;

						case "IE":
							_proxyMode = ProxyMode_e.IE;
							break;

						case "SPECIAL":
							_proxyMode = ProxyMode_e.SPECIAL;
							_proxyHost = argq.Dequeue();
							_proxyPort = int.Parse(argq.Dequeue());
							break;

						default:
							throw new Exception("不明なプロキシ利用モード");
					}
					continue;
				}
				if (ArgIs(argq, "/T"))
				{
					_timeoutMillis = int.Parse(argq.Dequeue());
					continue;
				}
				if (ArgIs(argq, "/M"))
				{
					switch (argq.Dequeue().ToUpper())
					{
						case "HEAD": _method = Method_e.HEAD; break;
						case "GET": _method = Method_e.GET; break;
						case "POST": _method = Method_e.POST; break;

						default:
							throw new Exception("不明なメソッド");
					}
					continue;
				}
				if (ArgIs(argq, "/U"))
				{
					_url = argq.Dequeue();
					continue;
				}
				if (ArgIs(argq, "/V"))
				{
					switch (argq.Dequeue())
					{
						case "10": _version = Version_e.HTTP_10; break;
						case "11": _version = Version_e.HTTP_11; break;

						default:
							throw new Exception("不明なバージョン");
					}
					continue;
				}
				if (ArgIs(argq, "/H"))
				{
					string[] headerField = new string[2];

					headerField[0] = argq.Dequeue();
					headerField[1] = argq.Dequeue();

					_headerFields.Add(headerField);
					continue;
				}
				if (ArgIs(argq, "/B"))
				{
					_body = ENCODING_SJIS.GetBytes(argq.Dequeue());
					continue;
				}
				if (ArgIs(argq, "/BF"))
				{
					_body = File.ReadAllBytes(argq.Dequeue());
					continue;
				}
				if (ArgIs(argq, "/RSF"))
				{
					_successfulFile = argq.Dequeue();
					continue;
				}
				if (ArgIs(argq, "/RHF"))
				{
					_resHeaderFieldsFile = argq.Dequeue();
					continue;
				}
				if (ArgIs(argq, "/RBF"))
				{
					_resBodyFile = argq.Dequeue();
					continue;
				}
				if (ArgIs(argq, "/RBFX"))
				{
					_resBodyFileSizeMax = long.Parse(argq.Dequeue());
					continue;
				}
				break;
			}

			if (argq.Count == 1)
			{
				_url = argq.Dequeue();
			}
			else if (argq.Count == 2)
			{
				_method = Method_e.POST;
				_url = argq.Dequeue();
				_body = File.ReadAllBytes(argq.Dequeue());
			}

			Perform();
		}

		private static int GetDefPort(string scheme)
		{
			if (scheme == "http")
				return 80;

			if (scheme == "https")
				return 443;

			throw new Exception("不明なスキーム");
		}

		private static void Perform()
		{
			// clear
			{
				File.Delete(_successfulFile);
				File.Delete(_resHeaderFieldsFile);
				File.Delete(_resBodyFile);
			}

			HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(_url);

			switch (_proxyMode)
			{
				case ProxyMode_e.DIRECT:
					hwr.Proxy = null;
					//hwr.Proxy = GlobalProxySelection.GetEmptyWebProxy(); // 古い実装
					break;

				case ProxyMode_e.IE:
					hwr.Proxy = WebRequest.GetSystemWebProxy();
					break;

				case ProxyMode_e.SPECIAL:
					hwr.Proxy = new WebProxy(_proxyHost, _proxyPort);
					break;

				default:
					throw null;
			}
			hwr.Timeout = _timeoutMillis;

			switch (_method)
			{
				case Method_e.HEAD: hwr.Method = "HEAD"; break;
				case Method_e.GET: hwr.Method = "GET"; break;
				case Method_e.POST: hwr.Method = "POST"; break;

				default:
					throw null;
			}
			switch (_version)
			{
				case Version_e.HTTP_10: hwr.ProtocolVersion = HttpVersion.Version10; break;
				case Version_e.HTTP_11: hwr.ProtocolVersion = HttpVersion.Version11; break;

				default:
					throw null;
			}
			foreach (string[] headerField in _headerFields)
			{
				string name = headerField[0];
				string value = headerField[1];

				if (IsSameIgnoreCase(name, "Content-Type"))
				{
					hwr.ContentType = value;
					continue;
				}
				if (IsSameIgnoreCase(name, "User-Agent"))
				{
					hwr.UserAgent = value;
					continue;
				}
				hwr.Headers.Add(name, value);
			}
			if (_body != null)
			{
				hwr.ContentLength = _body.Length;

				using (Stream w = hwr.GetRequestStream())
				{
					w.Write(_body, 0, _body.Length);
				}
			}

			// ステータスコード 200 以外の場合は例外を投げる ???

			using (WebResponse res = hwr.GetResponse())
			{
				using (FileStream fs = new FileStream(_resHeaderFieldsFile, FileMode.Create, FileAccess.Write))
				{
					int totalSize = 0;

					foreach (string name in res.Headers.Keys)
					{
						string line = name + ": " + res.Headers[name];

						// 2bs ???
						{
							line = line.Replace('\t', ' ');
							line = line.Replace('\r', ' ');
							line = line.Replace('\n', ' ');
						}

						line += "\r\n";

						byte[] bLine = Encoding.ASCII.GetBytes(line);

						if (500000 < bLine.Length) // 500 KB
							throw new Exception("Response header field too long");

						totalSize += bLine.Length;

						if (500000 < totalSize) // 500 KB
							throw new Exception("Response header too long");

						fs.Write(bLine, 0, bLine.Length);
					}
				}
				using (Stream r = res.GetResponseStream())
				using (FileStream fs = new FileStream(_resBodyFile, FileMode.Create, FileAccess.Write))
				{
					byte[] buff = new byte[20000000]; // 20 MB
					long totalSize = 0L;

					for (; ; )
					{
						int readSize = r.Read(buff, 0, buff.Length);

						if (readSize <= 0)
							break;

						totalSize += readSize;

						if (_resBodyFileSizeMax < totalSize)
							throw new Exception("Response body too long");

						fs.Write(buff, 0, readSize);
					}
				}
				CreateEmptyFile(_successfulFile);
			}
		}

		private static bool IsSameIgnoreCase(string a, string b)
		{
			return a.ToUpper() == b.ToUpper();
		}

		private static void CreateEmptyFile(string file)
		{
			using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
			{ }
		}
	}
}
