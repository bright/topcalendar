﻿using System;
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

namespace TopCalendar.UI.Infrastructure.Tests
{

	//public class LastViewIsActiveRegionBehavior_ForTests : LastViewIsActiveRegionBehavior
	//{
	//    public void RaiseOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	//    {
	//        this.OnCollectionChanged(sender,e);
	//    }
	//}

    public class LastViewIsActiveRegionBehavior_PrepareForTests : LastViewIsActiveRegionBehavior
    {
        public void RaiseOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged(sender,e);
        }
    }

	////    public ViewsCollectionMock(List<object> list)
	////    {
	////        _list = list;
	////    }

	////    public IEnumerator<object> GetEnumerator()
	////    {
	////        return _list.GetEnumerator();
	////    }

	////    IEnumerator IEnumerable.GetEnumerator()
	////    {
	////        return _list.GetEnumerator();
	////    }

	////    public event NotifyCollectionChangedEventHandler CollectionChanged;

	////    public bool Contains(object value)
	////    {
	////        return _list.Contains(value);
	////    }

	////    public void Add(object view)
	////    {
	////        _list.Add(view);
	////    }

	////    public bool Remove(object view)
	////    {
	////        return _list.Remove(view);
	////    }
	////}

	//public abstract class LastViewIsActiveRegionBehaviorSpecBase : observations_for_auto_created_sut_of_type<LastViewIsActiveRegionBehavior_ForTests>
	//{

    public abstract class LastViewIsActiveRegionBehaviorSpecBase :
        observations_for_auto_created_sut_of_type<LastViewIsActiveRegionBehavior_PrepareForTests>
    {
        
    }

	//    protected override void EstablishContext()
	//    {
	//        _region = MockRepository.GenerateMock<IRegion>();
	//        _region.Stub(x => x.Views).Return(_views);

	//        _views = MockRepository.GenerateMock<IViewsCollection>();

	//    }

	//    protected override void AfterSutCreation()
	//    {
	//        Sut.Region = _region;
	//        Sut.Attach();
	//    }
	//}

	//public class LastViewIsActiveRegionBehavior_when_we_add_new_view : LastViewIsActiveRegionBehaviorSpecBase
	//{
	//    protected override void Because()
	//    {
	//        Sut.RaiseOnCollectionChanged(null,new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
	//    }

    public class LastViewIsActiveRegionBehavior_when_we_add_new_view : LastViewIsActiveRegionBehaviorSpecBase
    {

        protected NotifyCollectionChangedEventArgs _notifyCollecitonChangedEventArgs;
        protected List<object> _newItems;
        protected List<object> _oldItems;

        protected override void Because()
        {
            Sut.RaiseOnCollectionChanged(null,new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
        }

        protected override void EstablishContext()
        {

            _notifyCollecitonChangedEventArgs =
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, _newItems, _oldItems);
        }

        //[Test]
        //public void gg()
        //{

        //}
    }
}
