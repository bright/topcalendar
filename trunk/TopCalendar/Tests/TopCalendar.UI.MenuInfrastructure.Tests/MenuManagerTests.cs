using System;
using Microsoft.Practices.Composite.Presentation.Events;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.Infrastructure.CommonCommands;
using TopCalendar.Utility.Tests;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;
using System.Collections.Generic;

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

	public abstract class abstract_when_adding_item_by_mm
		: observations_for_auto_created_sut_of_type<MenuManager>
	{
		protected const string TopLevel = "TopLevel";
		protected const string MenuName = "MenuName";
		protected const string Header = "Header";

		protected override void Because()
		{
			Sut.AddItemToMenu<CompositePresentationEvent<object>>(TopLevel, MenuName, Header);
		}
	}

	public class when_adding_item_by_mm : abstract_when_adding_item_by_mm
	{
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

	public abstract class when_adding_item_with_canExecuteHelper : abstract_when_adding_item_by_mm
	{
		protected IMenuProvider _menuProvider;
		protected CommandCanExecuteHelper _canExecute;

		protected override void EstablishContext()
		{
			_menuProvider = new MenuProvider(Dependency<IServiceLocator>());
			ProvideImplementationOf<IMenuProvider>(_menuProvider);

			_canExecute = new CommandCanExecuteHelper(false);
		}

		protected override void AfterSutCreation()
		{
			Sut.AddItemToMenu<CompositePresentationEvent<object>>(TopLevel, MenuName, Header, _canExecute);
		}
	}

	public class when_adding_item_which_cannot_execute : when_adding_item_with_canExecuteHelper
	{
		protected override void Because()
		{
			// because canExecute wasn't changed
		}

		[Test]
		public void command_should_be_enabled()
		{
			var command = _menuProvider.Menus[0].Items.As<List<MenuEntry>>()[0].Command;
			command.CanExecute(null).ShouldBeFalse();
		}
	}

	public class when_adding_item_which_can_execute : when_adding_item_with_canExecuteHelper
	{
		protected override void Because()
		{
			_canExecute.CanExecute = true;
		}

		[Test]
		public void command_should_be_enabled()
		{
			var command = _menuProvider.Menus[0].Items.As<List<MenuEntry>>()[0].Command;
			command.CanExecute(null).ShouldBeTrue();
		}
	}

	public class TestEvent : CompositePresentationEvent<string>{}	
	public class TestLabeledCommand : LabeledEventPublisherCommand<TestEvent,string> {
		
		public override string Header
		{
			get { return ""; }
		}
	}

	public class when_adding_labled_command : observations_for_auto_created_sut_of_type<MenuManager>
	{
		protected override void Because()
		{
			Sut.AddLabeledCommand<TestLabeledCommand,TestEvent,string>();
		}

		[Test]
		public void should_bind_command()
		{
			IsTypeBinded<ILabeledCommand<string>>().ShouldBeTrue();
		}
	}
}
