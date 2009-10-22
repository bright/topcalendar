namespace TopCalendar.UI.Modules.MonthViewer
{
	public class MonthViewPresentationModel : IMonthViewPersentationModel
	{
		public IMonthView View
		{
			get { return new MonthView(); }
		}
	}
}