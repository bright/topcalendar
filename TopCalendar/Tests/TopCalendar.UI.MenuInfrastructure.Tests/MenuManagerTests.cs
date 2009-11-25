using Microsoft.Practices.Composite.Presentation.Events;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using TopCalendar.Utility.Tests;

namespace TopCalendar.UI.MenuInfrastructure.Tests
{
    public class when_adding_top_level_by_mm : observations_for_auto_created_sut_of_type<MenuManager>
    {
    	private const string MenuName = "MenuName";
        private const string Header = "Header";

        protected override void Because()
        {
            Sut.AddTopLevelMenu(MenuName, Header);
        }

        [Test]
        public void should_call_provider()
        {
            Dependency<IMenuProvider>()
                .AssertWasCalled(mp =>
					mp.AddTopLevelMenu(Arg<MenuEntry>.Matches(
						Property.Value("Name", MenuName) && Property.Value("Header", Header)
					)));
        }
    }

	public class when_adding_item_by_mm : observations_for_auto_created_sut_of_type<MenuManager>
	{
		protected const string TopLevel = "TopLevel";
		protected const string MenuName = "MenuName";
		protected const string Header = "Header";

		protected override void Because()
		{
			Sut.AddItemToMenu<CompositePresentationEvent<object>>(TopLevel, MenuName, Header);
		}

		[Test]
		public void should_check_for_existence_of_top_level()
		{
			Dependency<IMenuProvider>()
				.AssertWasCalled(mp =>
					mp.GetTopLevelMenu(Arg<string>.Is.Equal(TopLevel)));
		}

		[Test]
		public void should_call_provider()
		{
			Dependency<IMenuProvider>()
				.AssertWasCalled(mp =>
					mp.AddItemToMenu(
						Arg<MenuEntry>.Is.Anything,
						Arg<MenuEntry>.Matches(
							Property.Value("Name", MenuName) && Property.Value("Header", Header)
						)
					));
		}
	}

	public class when_adding_item_to_nonexisting_top_level_by_mm : when_adding_item_by_mm
	{
		protected override void EstablishContext()
		{
			Dependency<IMenuProvider>().Stub(mp => mp.GetTopLevelMenu(TopLevel)).Return(null);
		}

		[Test]
		public void should_call_creating_new_top_level()
		{
			Dependency<IMenuProvider>()
				.AssertWasCalled(mp =>
					mp.AddTopLevelMenu(Arg<MenuEntry>.Matches(
						Property.Value("Name", TopLevel) && Property.Value("Header", Header)
					)));
		}
	}

	public class when_adding_item_to_existing_top_level_by_mm : when_adding_item_by_mm
	{
		private MenuEntry _topLevel;

		protected override void EstablishContext()
		{
			_topLevel = new MenuEntry() { Name = TopLevel, Header = TopLevel };
			Dependency<IMenuProvider>().Stub(mp => mp.GetTopLevelMenu(TopLevel)).Return(_topLevel);
		}

		[Test]
		public void should_not_call_creating_new_top_level()
		{
			Dependency<IMenuProvider>()
				.AssertWasNotCalled(mp =>
					mp.AddTopLevelMenu(Arg<MenuEntry>.Is.Anything));
		}
	}
}
