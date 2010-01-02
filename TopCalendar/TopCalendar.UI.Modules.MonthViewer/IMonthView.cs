using Microsoft.Practices.Composite;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.MonthViewer
{
	public interface IMonthView : IViewForModel<IMonthView, MonthViewPresentationModel>
	{
	}
}