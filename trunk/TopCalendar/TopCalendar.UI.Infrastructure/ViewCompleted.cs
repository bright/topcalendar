using Microsoft.Practices.Composite.Presentation.Events;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Infrastructure
{
	public class ViewCompleted<TView> : CompositePresentationEvent<TView>		
		where TView : IView
	{				
	}
}