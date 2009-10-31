namespace TopCalendar.Utility.UI
{
	public interface IView<TViewModel> : IView		
	{
		TViewModel ViewModel { get; set; }
	}

	public interface IView
	{
	}
}