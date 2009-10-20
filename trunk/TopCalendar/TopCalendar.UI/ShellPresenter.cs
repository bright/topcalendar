namespace TopCalendar.UI
{
	public class ShellPresenter
	{
		public ShellPresenter(IShellView shellView)
		{
			View = shellView;
		}

		public IShellView View { get; set; }
	}

	public interface IShellView
	{
		void ShowView();
	}
}