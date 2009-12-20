using Microsoft.Practices.Composite.Events;

namespace TopCalendar.Utility.Tests
{
	public abstract class observations_for_auto_created_sut_of_type_with_eventaggregator<TSut> : observations_for_auto_created_sut_of_type<TSut> where TSut : class
	{
		private EventAggregator _eventAggr;

		protected EventAggregator EventAggr
		{
			get
			{
				Check.Guard(_eventAggr != null, "You have forgotten to call base.EstablishContext() !");
				return _eventAggr;
			}
			private set { _eventAggr = value; }
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			EventAggr = new EventAggregator();
			ProvideImplementationOf<IEventAggregator>(EventAggr);
		}
	}
}