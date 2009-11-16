using System;
using NUnit.Framework;
using TopCalendar.Utility.UI;

namespace TopCalendar.Utility.Tests.UI
{
	public class TestNotify : NotifyPropertyChanged
	{
		private decimal _someValue;
		public decimal SomeValue
		{
			get { return _someValue; }
			set { 
				_someValue = value;
				OnPropertyChanged(()=> SomeValue);				
			}
		}
	}


	[TestFixture]
	public class when_calling_onPropertyChanged_with_expression
		: observations_for_sut_of_type<TestNotify>
	{
		private string _changePropertyName;
		private decimal _newValue;

		protected override void Because()
		{
			Sut.SomeValue = 12m;
		}

		protected override TestNotify CreateSut()
		{
			return new TestNotify();
		}

		protected override void AfterSutCreation()
		{
			Sut.PropertyChanged += Sut_PropertyChanged;
		}

		protected override void AfterEachObservation()
		{
			Sut.PropertyChanged -= Sut_PropertyChanged;
		}

		void Sut_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_changePropertyName = e.PropertyName;
			_newValue = Sut.SomeValue;
		}

		[Test]
		public void should_notify_property_has_changed()
		{
			_changePropertyName.ShouldEqual("SomeValue");
		}

		[Test]
		public void should_have_new_value_of_property_set_correctly()
		{
			_newValue.ShouldEqual(12m);
		}

	}

}