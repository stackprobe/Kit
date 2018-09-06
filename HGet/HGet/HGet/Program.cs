using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace HGet
{
	class Program
	{
		private static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		static void Main(string[] args)
		{
			try
			{
				if (1 <= args.Length && args[0].ToUpper() == "//R")
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

				if (_errorKeyWait)
				{
					Console.WriteLine("Press any key...");
					Console.ReadKey();
				}
			}
			if (_endKeyWait)
			{
				Console.WriteLine("Press any key...");
				Console.ReadKey();
			}
		}

		private static bool ArgIs(Queue<string> argq, string spell)
		{
			if (1 <= argq.Count && IsSameIgnoreCase(argq.Peek(), spell))
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
		private static bool _tls12Enabled = true;
		private static int _connectionTimeoutMillis = 20000; // 接続開始から、応答ヘッダを受信し終えるまでのタイムアウト
		private static int _timeoutMillis = 30000;           // 接続開始から、全て通信し終えるまでのタイムアウト
		private static int _noTrafficTimeoutMillis = 15000;  // 応答ボディ受信中の無通信タイムアウト
		private static Method_e _method = Method_e.GET;
		private static string _url = "http://localhost/";
		private static Version_e _version = Version_e.HTTP_11;
		private static List<string[]> _headerFields = new List<string[]>();
		private static byte[] _body = null; // null == no body
		private static string _bodyFile = null; // null == no body-2
		private static byte[] _bodyTrailer = null; // null == no body-3
		private static string _successfulFile;
		private static string _resHeaderFieldsFile;
		private static string _resBodyFile;
		private static long _resBodyFileSizeMax = 20000000L; // 20 MB
		private static bool _endKeyWait = false;
		private static bool _errorKeyWait = false;

		private static void Main2(string[] args)
		{
			{
				string dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

				_successfulFile = Path.Combine(dir, "HGet_successfulFlag.tmp");
				_resHeaderFieldsFile = Path.Combine(dir, "HGet_resHeaderFields.tmp");
				_resBodyFile = Path.Combine(dir, "HGet_resBody.tmp");
			}

			// clear *.tmp
			{
				File.Delete(_successfulFile);
				File.Delete(_resHeaderFieldsFile);
				File.Delete(_resBodyFile);
			}

			Queue<string> argq = new Queue<string>(args);

			while (ArgIs(argq, "/-") == false)
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
				if (ArgIs(argq, "/TLS12"))
				{
					_tls12Enabled = true;
					continue;
				}
				if (ArgIs(argq, "/-TLS12"))
				{
					_tls12Enabled = false;
					continue;
				}
				if (ArgIs(argq, "/CT"))
				{
					_connectionTimeoutMillis = int.Parse(argq.Dequeue());
					continue;
				}
				if (ArgIs(argq, "/To"))
				{
					_timeoutMillis = int.Parse(argq.Dequeue());
					continue;
				}
				if (ArgIs(argq, "/NTT"))
				{
					_noTrafficTimeoutMillis = int.Parse(argq.Dequeue());
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
					_body = StringToBody(argq.Dequeue(), ENCODING_SJIS);
					continue;
				}
				if (ArgIs(argq, "/BF"))
				{
					_body = File.ReadAllBytes(argq.Dequeue());
					continue;
				}
				if (ArgIs(argq, "/F"))
				{
					_bodyFile = argq.Dequeue();
					continue;
				}
				if (ArgIs(argq, "/T"))
				{
					_bodyTrailer = StringToBody(argq.Dequeue(), ENCODING_SJIS);
					continue;
				}
				if (ArgIs(argq, "/TF"))
				{
					_bodyTrailer = File.ReadAllBytes(argq.Dequeue());
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
				if (ArgIs(argq, "/K"))
				{
					_endKeyWait = true;
					continue;
				}
				if (ArgIs(argq, "/K-"))
				{
					_errorKeyWait = true;
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
				//_body = File.ReadAllBytes(argq.Dequeue());
				_bodyFile = argq.Dequeue();
			}

			if (1 <= argq.Count)
				throw new Exception("Unknown command-line option: " + argq.Peek());

			Perform();
		}

		private static int GetDefPort(string scheme)
		{
			if (scheme == "http")
				return 80;

			if (scheme == "https")
				return 443;

			throw new Exception("Unknown scheme: " + scheme);
		}

		private static void Perform()
		{
			// clear
			{
				File.Delete(_successfulFile);
				File.Delete(_resHeaderFieldsFile);
				File.Delete(_resBodyFile);
			}

			// どんな証明書も許可する。
			ServicePointManager.ServerCertificateValidationCallback =
				(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

			if (_tls12Enabled)
			{
				// https://blogs.perficient.com/2016/04/28/tsl-1-2-and-net-support/
				// .NET 4.0. TLS 1.2 is not supported, but if you have .NET 4.5 (or above) installed on the system then you still
				// can opt in for TLS 1.2 even if your application framework doesn’t support it.
				ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
			}

			HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(_url);
			DateTime startedTime = DateTime.Now;
			TimeSpan timeoutSpan = TimeSpan.FromMilliseconds(_timeoutMillis);

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
			hwr.Timeout = _connectionTimeoutMillis;

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
				if (IsSameIgnoreCase(name, "Host"))
				{
					hwr.Host = value;
					continue;
				}
				if (IsSameIgnoreCase(name, "Accept"))
				{
					hwr.Accept = value;
					continue;
				}
				hwr.Headers.Add(name, value);
			}
			if (_body != null || _bodyFile != null || _bodyTrailer != null)
			{
				long total = 0L;

				if (_body != null)
				{
					total += _body.Length;
				}
				if (_bodyFile != null)
				{
					total += new FileInfo(_bodyFile).Length;
				}
				if (_bodyTrailer != null)
				{
					total += _bodyTrailer.Length;
				}
				hwr.ContentLength = total;

				using (Stream w = hwr.GetRequestStream())
				{
					if (_body != null)
					{
						w.Write(_body, 0, _body.Length);
					}
					if (_bodyFile != null)
					{
						using (FileStream r = new FileStream(_bodyFile, FileMode.Open, FileAccess.Read))
						{
							byte[] buff = new byte[20000000]; // 20 MB

							for (; ; )
							{
								int readSize = r.Read(buff, 0, buff.Length);

								if (readSize <= 0)
									break;

								w.Write(buff, 0, readSize);
							}
						}
					}
					if (_bodyTrailer != null)
					{
						w.Write(_body, 0, _bodyTrailer.Length);
					}
					w.Flush();
				}
			}

			// ステータスコード 301 のとき転送先のコンテンツを勝手に取ってきてくれる。

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
					r.ReadTimeout = _noTrafficTimeoutMillis; // この時間経過すると r.Read() が例外を投げる。

					byte[] buff = new byte[20000000]; // 20 MB
					long totalSize = 0L;

					for (; ; )
					{
						int readSize = r.Read(buff, 0, buff.Length);

						if (readSize <= 0)
							break;

						if (timeoutSpan < DateTime.Now - startedTime)
							throw new Exception("Response timed out");

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

		private const string S_ESCAPE = "\x1b";

		private static byte[] StringToBody(string str, Encoding encoding)
		{
			str = str.Replace("$$", S_ESCAPE);
			str = str.Replace("$t", "\t");
			str = str.Replace("$r", "\r");
			str = str.Replace("$n", "\n");
			str = str.Replace("$s", " ");
			str = str.Replace(S_ESCAPE, "$");

			return encoding.GetBytes(str);
		}
	}
}
