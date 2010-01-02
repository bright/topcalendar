using System;
using NHibernate;

namespace TopCalendar.Server.DataLayer
{
	public class WithinNhTransactionWorker : IWithinTransactionWorker 
	{
		private readonly ISession _session;

		public WithinNhTransactionWorker(ISession session)
		{
			_session = session;
		}

		public void Perform(Action work)
		{
			using(var t = _session.BeginTransaction())
			{
				work();
				t.Commit();
			}
		}
	}

	public interface IWithinTransactionWorker
	{
		void Perform(Action work);
	}
}