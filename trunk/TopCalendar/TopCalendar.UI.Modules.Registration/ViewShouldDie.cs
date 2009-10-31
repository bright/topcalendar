using Microsoft.Practices.Composite.Presentation.Events;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Registration
{
	public class ViewShouldDie<TView> : CompositePresentationEvent<TView>
		where TView : IView
	{				
	}
}