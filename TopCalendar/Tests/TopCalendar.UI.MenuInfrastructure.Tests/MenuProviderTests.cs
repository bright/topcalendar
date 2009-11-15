using NUnit.Framework;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.MenuInfrastructure.Tests
{
	public class when_adding_top_level_by_mp : observations_for_auto_created_sut_of_type<MenuProvider>
	{
		private const string MenuName = "MenuName";
		private const string Header = "Header";

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
	}

	public class when_adding_null_top_level_by_mp : observations_for_auto_created_sut_of_type<MenuProvider>
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
	}

	public abstract class when_adding_item_by_mp : observations_for_auto_created_sut_of_type<MenuProvider>
	{
		private const string _topLevel = "TopLevel";
		private const string _menuName = "MenuName";
		private const string _header = "Header";

		protected MenuEntry topLevel;
		protected MenuEntry menuEntry;

		protected override void EstablishContext()
		{
			topLevel = new MenuEntry() {Name = _topLevel, Header = _header};
			menuEntry = new MenuEntry() {Name = _menuName, Header = _header};
		}

		protected override void AfterSutCreation()
		{
			// wywalam defaultowe wpisy
			Sut.Menus.Clear();

			// dodaje top-level
			Sut.AddTopLevelMenu(topLevel);
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
	}

}
