using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.WeekViewer
{
	public class WeekViewPresentationModel : PresentationModelFor<IWeekView>
	{
		public WeekViewPresentationModel(IWeekView view, IWeekTaskLoader taskLoader) : base(view)
		{

			_view.ViewModel = this;
		}
	}

	public interface IWeekTaskLoader
	{
	}

	public interface IWeekView : IViewForModel<IWeekView,WeekViewPresentationModel>
	{
	}
}