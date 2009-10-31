namespace TopCalendar.Utility.UI
{
	public abstract class PresentationModelFor<TView> 
		: NotifyPropertyChanged, IPresentationModelFor<TView>
		where TView : IView
	{
		protected TView _view;

		protected PresentationModelFor(TView view)
		{
			_view = view;
		}

		public virtual TView View { 
			get
			{
				return _view;
			}  
		}		
	}
}