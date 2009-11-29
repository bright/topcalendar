using NUnit.Framework;
using TopCalendar.Utility.Tests;
using System.Collections.Specialized;

namespace TopCalendar.UI.MenuInfrastructure.Tests
{
	public abstract class observations_for_MenuProvider : observations_for_auto_created_sut_of_type<MenuProvider>
	{
		protected const string MenuName = "MenuName";
		protected const string Header = "Header";

		protected bool _notified = false;

		protected override void AfterSutCreation()
		{
			Sut.Menus.CollectionChanged += NotifyChange;
		}

		private void NotifyChange(object sender, NotifyCollectionChangedEventArgs e)
		{
			_notified = true;
		}
	}

	public class when_adding_top_level_by_mp : observations_for_MenuProvider
	{
		private MenuEntry _menuEntry;

		protected override void EstablishContext()
		{
			_menuEntry = new MenuEntry() { Name = MenuName, Header = Header };
		}

		protected override void Because()
		{
			Sut.AddTopLevelMenu(_menuEntry);
		}

		[Test]
		public void should_be_saved()
		{
			Sut.Menus.Contains(_menuEntry).ShouldBeTrue();
		}

		[Test]
		public void should_be_returned_by_getter()
		{
			Sut.GetTopLevelMenu(MenuName).ShouldBeTheSameAs(_menuEntry);
		}

		[Test]
		public void notification_should_be_raised()
		{
			_notified.ShouldBeTrue();
		}
	}

	public class when_adding_null_top_level_by_mp : observations_for_MenuProvider
	{
		protected override void Because()
		{
			Sut.AddTopLevelMenu(null);
		}

		[Test]
		public void should_not_add_anything()
		{
			Assert.IsEmpty(Sut.Menus);
		}

		[Test]
		public void should_not_be_returned_by_getter()
		{
			Sut.GetTopLevelMenu("doesn't exist").ShouldBeNull();
		}

		[Test]
		public void notification_should_not_be_raised()
		{
			_notified.ShouldBeFalse();
		}
	}

	public abstract class when_adding_item_by_mp : observations_for_MenuProvider
	{
		private const string _topLevel = "TopLevel";

		protected MenuEntry topLevel;
		protected MenuEntry menuEntry;

		protected override void EstablishContext()
		{
			topLevel = new MenuEntry() {Name = _topLevel, Header = Header};
			menuEntry = new MenuEntry() {Name = MenuName, Header = Header};
		}

		protected override void AfterSutCreation()
		{
			// dodaje top-level
			Sut.AddTopLevelMenu(topLevel);

			base.AfterSutCreation();
		}
	}

	public class when_adding_null_item_by_mp : when_adding_item_by_mp
	{
		protected override void Because()
		{
			Sut.AddItemToMenu(topLevel, null);
		}

		[Test]
		public void should_not_add_anything()
		{
			Assert.AreEqual(Sut.GetTopLevelMenu(topLevel.Name).Items.Count, 0);
		}

		[Test]
		public void notification_should_not_be_raised()
		{
			_notified.ShouldBeFalse();
		}
	}

	public class when_adding_valid_item_by_mp : when_adding_item_by_mp
	{
		protected override void Because()
		{
			Sut.AddItemToMenu(topLevel, menuEntry);
		}

		[Test]
		public void should_save_entry()
		{
			Assert.AreEqual(Sut.GetTopLevelMenu(topLevel.Name).Items.Count, 1);
		}

		[Test]
		public void notification_should_be_raised()
		{
			_notified.ShouldBeTrue();
		}
	}

}
