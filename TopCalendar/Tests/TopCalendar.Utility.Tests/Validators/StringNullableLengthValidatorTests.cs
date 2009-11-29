using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using NUnit.Framework;
using TopCalendar.Utility.Validators;

namespace TopCalendar.Utility.Tests.Validators
{

	public class TestStringValidatorClass
	{
		[StringNullableLengthValidator(1,10)]
		public string ToValidate { get; set; }
	}
	
	public abstract class observations_for_validating_string_with_nullable_string_validator : observations_for_sut
	{
		protected TestStringValidatorClass _testClass;
		protected ValidationResults _results;

		protected override void Because()
		{
			_results = Validation.Validate(_testClass);
		}		
	}

	public class when_validating_null_string_with_nullable_lenght_validator
		: observations_for_validating_string_with_nullable_string_validator
	{
		protected override void EstablishContext()
		{
			_testClass = new TestStringValidatorClass();
		}

		[Test]
		public void should_validate()
		{
			_results.ShouldHaveCount(0);
		}
	}	

	public class when_validating_not_null_string_with_nullable_string_validator
		: observations_for_validating_string_with_nullable_string_validator
	{		
		protected override void EstablishContext()
		{
			_testClass = new TestStringValidatorClass(){ToValidate="valid"};
		}

		[Test]
		public void should_validate()
		{
			_results.ShouldHaveCount(0);
		}
	}

	public class when_validating_not_null_but_invalid_string_with_nullable_string_validator 
		: observations_for_validating_string_with_nullable_string_validator
	{
		protected override void EstablishContext()
		{
			_testClass = new TestStringValidatorClass() { ToValidate = "way too long string" };
		}

		[Test]
		public void should_not_validate()
		{
			_results.ShouldHaveCount(1);
		}
	}

	
}