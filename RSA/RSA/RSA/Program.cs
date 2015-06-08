using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace RSA
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
				EndProc(1);
			}
			EndProc(0);
		}

		private static void EndProc(int endCode)
		{
			Console.WriteLine("endCode: " + endCode);
			Environment.Exit(endCode);
		}

		private readonly Encoding ENCODING_KEYFILE = Encoding.ASCII;
		private const bool F_OAEP = true;

		private void Main2(string[] args)
		{
			switch (args[0].ToUpper())
			{
				case "/G":
					using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(int.Parse(args[1])))
					{
						string publicKeyFile = args[2];
						string privateKeyFile = args[3];

						string publicKey = rsa.ToXmlString(false);
						string privateKey = rsa.ToXmlString(true);

						File.WriteAllText(publicKeyFile, publicKey, ENCODING_KEYFILE);
						File.WriteAllText(privateKeyFile, privateKey, ENCODING_KEYFILE);
					}
					break;

				case "/E":
					using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
					{
						string publicKey = File.ReadAllText(args[1], ENCODING_KEYFILE);
						byte[] plainData = File.ReadAllBytes(args[2]);
						byte[] cipherData;

						rsa.FromXmlString(publicKey);
						cipherData = rsa.Encrypt(plainData, F_OAEP);

						File.WriteAllBytes(args[3], cipherData);
					}
					break;

				case "/D":
					using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
					{
						string privateKey = File.ReadAllText(args[1], ENCODING_KEYFILE);
						byte[] cipherData = File.ReadAllBytes(args[2]);
						byte[] plainData;

						rsa.FromXmlString(privateKey);
						plainData = rsa.Decrypt(cipherData, F_OAEP);

						File.WriteAllBytes(args[3], plainData);
					}
					break;
			}
		}
	}
}
