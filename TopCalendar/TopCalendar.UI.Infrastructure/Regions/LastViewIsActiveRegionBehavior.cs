using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite;
using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
using Microsoft.Practices.Composite.Regions;

namespace TopCalendar.UI.Infrastructure.Regions
{

    public class LastViewIsActiveRegionBehavior : IRegionBehavior
    {
        /// <summary>
        /// Name that identifies the <see cref="RegionActiveAwareBehavior"/> behavior in a collection of <see cref="IRegionBehavior"/>.
        /// </summary>
        public const string BehaviorKey = "LastIsActive";

    	/// <summary>
        /// The region that this behavior is extending
        /// </summary>
        public IRegion Region { get; set; }

        /// <summary>
        /// Attaches the behavior to the specified region
        /// </summary>
        public void Attach()
        {
            INotifyCollectionChanged collection = GetCollection();
            if (collection != null)
            {
                collection.CollectionChanged += OnCollectionChanged;
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
            }
        }

        protected readonly List<object> LatestActiveViews = new List<object>();

        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (!Region.ActiveViews.Contains(e.NewItems[0]))
                    LatestActiveViews.Add(Region.ActiveViews.First());

                Region.Activate(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {

                if (e.OldItems.Count > 0)
                {
                    var deletedView = e.OldItems[e.OldItems.Count - 1];
                    if (!LatestActiveViews.Remove(deletedView))              // if we remove view that is active
                    {
                        if (LatestActiveViews.Count > 0)
                        {
                            var viewToActivate = LatestActiveViews[LatestActiveViews.Count - 1];

                            Region.Activate(viewToActivate);
                            LatestActiveViews.Remove(viewToActivate);
                        }
                    }

                }

            }

            // May need to handle other action values (reset, replace). Currently the ViewsCollection class does not raise CollectionChanged with these values.
        }

        private INotifyCollectionChanged GetCollection()
        {
            return Region.Views;
        }
    }
}
