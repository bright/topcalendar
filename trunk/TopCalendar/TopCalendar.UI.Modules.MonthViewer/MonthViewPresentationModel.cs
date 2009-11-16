namespace TopCalendar.UI.Modules.MonthViewer
{
	public class MonthViewPresentationModel : IMonthViewPresentationModel
	{
		public IMonthView View
		{
			get { return new MonthView(); }
		}
	}    

}