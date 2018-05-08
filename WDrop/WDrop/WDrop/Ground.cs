using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;

namespace WDrop
{
	public class Gnd
	{
		public static Gnd I;

		public string ParentProgFile;
		public int ParentProcId;
		public string ParentAliveMutexName;
		public string OutFile;
	}
}
