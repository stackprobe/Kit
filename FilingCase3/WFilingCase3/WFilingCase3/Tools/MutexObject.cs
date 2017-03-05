using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MutexObject : IDisposable
	{
		private Mutex _m;

		public MutexObject(string name)
		{
			_m = new Mutex(false, name);
		}

		public void waitForever()
		{
			_m.WaitOne();
		}

		public bool waitForMillis(int millis)
		{
			return _m.WaitOne(millis);
		}

		public void release()
		{
			_m.ReleaseMutex();
		}

		public void Dispose()
		{
			if (_m != null)
			{
				_m.Dispose();
				_m = null;
			}
		}

		public class Section : IDisposable
		{
			private MutexObject _m;
			private bool _auto;

			public Section(MutexObject m)
			{
				_m = m;

				doLock();
			}

			public Section(string name)
			{
				_m = new MutexObject(name);
				_auto = true;

				doLock();
			}

			private void doLock()
			{
				_m.waitForever();
			}

			private void doUnlock()
			{
				_m.release();
			}

			public void Dispose()
			{
				if (_m != null)
				{
					doUnlock();

					if (_auto)
						_m.Dispose();

					_m = null;
				}
			}
		}
	}
}
