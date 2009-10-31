namespace TopCalendar.Utility.UI
{
	public interface IViewForModel<TView, TViewModel> : IView<TViewModel>
		where TViewModel : PresentationModelFor<TView>
		where TView : IView<TViewModel>
	{		
	}
}