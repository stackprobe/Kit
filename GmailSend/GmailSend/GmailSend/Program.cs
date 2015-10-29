using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;

namespace GmailSend
{
	/// <summary>
	/// 5.5.1 Authentication Required. Learm more at ->
	/// アカウント情報 / Google へのログイン /
	/// 安全性の低いアプリの許可：有効
	/// </summary>
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

		private Queue<string> _argq;

		private bool ArgIs(string spell)
		{
			if (_argq.Peek().ToLower() == spell.ToLower())
			{
				_argq.Dequeue();
				return true;
			}
			return false;
		}

		private void Main2(string[] args)
		{
			_argq = new Queue<string>(args);

			string successfulFile = null;
			string errorLogFile = null;

			using (MailMessage mm = new MailMessage())
			using (SmtpClient sc = new SmtpClient())
			{
				sc.DeliveryMethod = SmtpDeliveryMethod.Network;

				sc.Host = "smtp.gmail.com";
				sc.Port = 587;
				sc.EnableSsl = true;

				while (1 <= _argq.Count)
				{
					int argc = _argq.Count;

					if (ArgIs("/SF"))
					{
						successfulFile = _argq.Dequeue();
					}
					if (ArgIs("/ELF"))
					{
						errorLogFile = _argq.Dequeue();
					}

					if (ArgIs("/F"))
					{
						mm.From = new MailAddress(_argq.Dequeue());
					}
					if (ArgIs("/To"))
					{
						mm.To.Add(new MailAddress(_argq.Dequeue()));
					}
					if (ArgIs("/CC"))
					{
						mm.CC.Add(new MailAddress(_argq.Dequeue()));
					}
					if (ArgIs("/BCC"))
					{
						mm.Bcc.Add(new MailAddress(_argq.Dequeue()));
					}
					if (ArgIs("/S"))
					{
						mm.Subject = _argq.Dequeue();
					}
					if (ArgIs("/B"))
					{
						mm.Body = GetText(_argq.Dequeue());
					}
					if (ArgIs("/A"))
					{
						mm.Attachments.Add(new Attachment(_argq.Dequeue()));
					}
					if (ArgIs("/C"))
					{
						string user = _argq.Dequeue();
						string password = _argq.Dequeue();

						sc.Credentials = new System.Net.NetworkCredential(user, password);
					}
					if (ArgIs("/Host"))
					{
						sc.Host = _argq.Dequeue();
					}
					if (ArgIs("/Port"))
					{
						sc.Port = int.Parse(_argq.Dequeue());
					}
					if (ArgIs("/-SSL"))
					{
						sc.EnableSsl = false;
					}

					if (argc == _argq.Count)
					{
						throw new Exception("不明な引数：" + _argq.Peek());
					}
				}

				try
				{
					sc.Send(mm);

					if (successfulFile != null)
					{
						File.WriteAllBytes(successfulFile, new byte[0]);
					}
				}
				catch (Exception e)
				{
					if (errorLogFile != null)
					{
						File.WriteAllText(errorLogFile, "" + e, ENCODING_SJIS);
					}
					else
					{
						Console.WriteLine(e);
					}
				}
			}
		}

		private readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		private string GetText(string prm)
		{
			if (prm.StartsWith("**"))
			{
				return prm.Substring(1);
			}
			if (prm.StartsWith("*"))
			{
				return File.ReadAllText(prm.Substring(1), ENCODING_SJIS);
			}
			return prm;
		}
	}
}
