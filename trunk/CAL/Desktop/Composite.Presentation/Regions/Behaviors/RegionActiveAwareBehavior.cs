//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Practices.Composite.Regions;
using System.Linq;

namespace Microsoft.Practices.Composite.Presentation.Regions.Behaviors
{
    /// <summary>
    /// Behavior that monitors a <see cref="IRegion"/> object and 
    /// changes the value for the <see cref="IActiveAware.IsActive"/> property when
    /// an object that implements <see cref="IActiveAware"/> gets added or removed 
    /// from the collection.
    /// </summary>
    public class RegionActiveAwareBehavior : IRegionBehavior
    {
        /// <summary>
        /// Name that identifies the <see cref="RegionActiveAwareBehavior"/> behavior in a collection of <see cref="IRegionBehavior"/>.
        /// </summary>
        public const string BehaviorKey = "ActiveAware";

        /// <summary>
        /// The region that this behavior is extending
        /// </summary>
        public IRegion Region { get; set; }

        /// <summary>
        /// Attaches the behavior to the specified region
        /// </summary>
        public void Attach()
        {
            INotifyCollectionChanged collection = this.GetCollection();
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
            INotifyCollectionChanged collection = this.GetCollection();
            if (collection != null)
            {
                collection.CollectionChanged -= OnCollectionChanged;
            }
        }

        private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object item in e.NewItems)
                {
                    IActiveAware activeAware = item as IActiveAware;
                    if (activeAware != null)
                    {
                        activeAware.IsActive = true;
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object item in e.OldItems)
                {
                    IActiveAware activeAware = item as IActiveAware;
                    if (activeAware != null)
                    {
                        activeAware.IsActive = false;
                    }
                }
            }

            // May need to handle other action values (reset, replace). Currently the ViewsCollection class does not raise CollectionChanged with these values.
        }

        private INotifyCollectionChanged GetCollection()
        {

            return this.Region.ActiveViews;
        }
    }


    public class LastViewIsActiveBehavior : IRegionBehavior
    {
        /// <summary>
        /// Name that identifies the <see cref="RegionActiveAwareBehavior"/> behavior in a collection of <see cref="IRegionBehavior"/>.
        /// </summary>
        public const string BehaviorKey = "LastIsActive";

        protected IActiveAware lastView;

        /// <summary>
        /// The region that this behavior is extending
        /// </summary>
        public IRegion Region { get; set; }

        /// <summary>
        /// Attaches the behavior to the specified region
        /// </summary>
        public void Attach()
        {
            INotifyCollectionChanged collection = this.GetCollection();
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
            INotifyCollectionChanged collection = this.GetCollection();
            if (collection != null)
            {
                collection.CollectionChanged -= OnCollectionChanged;
            }
        }

        protected List<object> LatestActiveViews = new List<object>();

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if(!this.Region.ActiveViews.Contains(e.NewItems[0]))
                    LatestActiveViews.Add(this.Region.ActiveViews.First());

                this.Region.Activate(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {

                if (e.OldItems.Count > 0)
                {
                    var deletedView = e.OldItems[e.OldItems.Count - 1];
                    if(!LatestActiveViews.Remove(deletedView))              // if we remove view that is active
                    {
                        if (LatestActiveViews.Count > 0)
                        {
                            var viewToActivate = LatestActiveViews[LatestActiveViews.Count - 1];

                            this.Region.Activate(viewToActivate);
                            LatestActiveViews.Remove(viewToActivate);
                        }
                    }

                }

            }

            // May need to handle other action values (reset, replace). Currently the ViewsCollection class does not raise CollectionChanged with these values.
        }

        private INotifyCollectionChanged GetCollection()
        {
            return this.Region.Views;
        }
    }
}