using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite;
using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.Events;

namespace TopCalendar.UI.Infrastructure.Regions
{

    public class LastViewIsActiveRegionBehavior : IRegionBehavior
    {
		private IEventAggregator _eventAggregator;

        /// <summary>
        /// Name that identifies the <see cref="RegionActiveAwareBehavior"/> behavior in a collection of <see cref="IRegionBehavior"/>.
        /// </summary>
        public const string BehaviorKey = "LastIsActive";

    	/// <summary>
        /// The region that this behavior is extending
        /// </summary>
        public IRegion Region { get; set; }

		public LastViewIsActiveRegionBehavior(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

        /// <summary>
        /// Attaches the behavior to the specified region
        /// </summary>
        public void Attach()
        {
            INotifyCollectionChanged collection = GetCollection();
            if (collection != null)
            {
                collection.CollectionChanged += OnCollectionChanged;
				_eventAggregator.GetEvent<RegistrationCompletedEvent>().Subscribe(UnloadFirstView);
            }
        }

        /// <summary>
        /// Detaches the behavior from the <see cref="INotifyCollectionChanged"/>.
        /// </summary>
        public void Detach()
        {
            INotifyCollectionChanged collection = GetCollection();
            if (collection != null)
            {
                collection.CollectionChanged -= OnCollectionChanged;
				_eventAggregator.GetEvent<RegistrationCompletedEvent>().Unsubscribe(UnloadFirstView);
            }
        }

		protected object LastActiveView;

		private void UnloadFirstView(object nothing)
		{
			LastActiveView = null;
		}

        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
				// nie pytajcie co to, sam już nie wiem
				// udaje ze dziala
				if (LastActiveView == null)
				{
					LastActiveView = e.OldItems[0];
				}
				else
				{
					Region.Activate(LastActiveView);
					LastActiveView = null;
				}
            }

            // May need to handle other action values (reset, replace). Currently the ViewsCollection class does not raise CollectionChanged with these values.
        }

        private INotifyCollectionChanged GetCollection()
        {
            return Region.ActiveViews;
        }
    }
}
