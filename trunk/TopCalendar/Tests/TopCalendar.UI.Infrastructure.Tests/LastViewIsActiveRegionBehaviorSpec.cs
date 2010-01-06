using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.UI.Infrastructure.Regions;
using TopCalendar.Utility.Tests;
using Microsoft.Practices.Composite.Events;

namespace TopCalendar.UI.Infrastructure.Tests
{

	public class LastViewIsActiveRegionBehavior_PrepareForTests : LastViewIsActiveRegionBehavior
    {
		public LastViewIsActiveRegionBehavior_PrepareForTests(IEventAggregator e) : base(e) { }

        public void RaiseOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged(sender,e);
        }
    }

	

	public abstract class LastViewIsActiveRegionBehaviorSpecBase :
		observations_for_auto_created_sut_of_type<LastViewIsActiveRegionBehavior_PrepareForTests>
	{

	}

	

	//public class LastViewIsActiveRegionBehavior_when_we_add_new_view : LastViewIsActiveRegionBehaviorSpecBase
	//{

	//    protected NotifyCollectionChangedEventArgs _notifyCollecitonChangedEventArgs;
	//    protected List<object> _newItems;
	//    protected List<object> _oldItems;

	//    protected override void Because()
	//    {
	//        Sut.RaiseOnCollectionChanged(null,new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
	//    }

	//    protected override void EstablishContext()
	//    {

	//        _notifyCollecitonChangedEventArgs =
	//            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _newItems, _oldItems);
	//    }

	//    [Test]
	//    public void gg()
	//    {

	//    }
	//}
}
