namespace TopCalendar.Utility.UI
{
	public interface  IPresentationModelFor<TView>
		where TView: IView
	{
		TView View { get; }
	}
}