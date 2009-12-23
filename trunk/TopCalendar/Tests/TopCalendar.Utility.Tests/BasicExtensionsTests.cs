using System;
using System.Linq;
using NUnit.Framework;
using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Utility.BasicExtensions.Tests
{

	[TestFixture]
	public class BasicExtensionsTests
	{

		[Test]
		public void AtMonthEnd_should_return_month_end_date()
		{
			var test = new DateTime(2009, 11, 16);
			var result = test.AtMonthEnd();

			result.Year.ShouldEqual(test.Year);
			result.Month.ShouldEqual(test.Month);
			result.Day.ShouldEqual(30);
			result.Hour.ShouldEqual(23);
			result.Minute.ShouldEqual(59);
		}

		[Test]
		public void AtWeekStart_should_return_week_start_date()
		{
			var date = new DateTime(2009, 12, 20);
			var result = date.AtWeekStart();
			result.Year.ShouldEqual(date.Year);
			result.Month.ShouldEqual(date.Month);
			result.Day.ShouldEqual(14);
		}

		[Test]
		public void Range_should_generate_enumerable_of_proper_size()
		{
			var start = DateTime.Now;
			var stop = DateTime.Now.AddDays(1).AddHours(1);
			start.Range(stop, TimeSpan.FromHours(1))
				.Each(dt => dt.IsBetween(start, stop).ShouldBeTrue())
				.Count().ShouldEqual(25);
		}

		[Test]
		public void AtWeekEnd_should_return_week_end_date()
		{
			var date = new DateTime(2009, 12, 20);
			var result = date.AtWeekEnd();
			result.Year.ShouldEqual(date.Year);
			result.Month.ShouldEqual(date.Month);
			result.Day.ShouldEqual(20);
		}

		[Test]
		public void AtMonthStart_should_return_month_start_date()
		{
			var test = new DateTime(3121, 11, 10);
			var result = test.AtMonthStart();
			result.Year.ShouldEqual(test.Year);
			result.Month.ShouldEqual(test.Month);
			result.Day.ShouldEqual(1);
		}


		[Test]
		public void If_should_return_original_value()
		{
			var test = new TestObject { Boolean = true };

			"test".If(() => test.Boolean).ShouldEqual("test");
		}

		[Test]
		public void If_should_return_empty_string_value()
		{
			var test = new TestObject { Boolean = false };

			"test".If(() => test.Boolean).ShouldEqual("");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void If_should_return_an_ArgumentException()
		{
			var test = new TestObject { Value = 1 };

			"test".If(() => test.Value == 1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void If_should_also_return_an_ArgumentException()
		{
			var test = new TestObject { Boolean = true };

			"test".If(() => test.Boolean && test.Boolean);
		}

		[Test]
		public void IfNot_should_return_original_value()
		{
			var test = new TestObject { Boolean = true };

			"test".IfNot(() => test.Boolean).ShouldEqual("");
		}

		[Test]
		public void IfNot_should_return_empty_string_value()
		{
			var test = new TestObject { Boolean = false };

			"test".IfNot(() => test.Boolean).ShouldEqual("test");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IfNot_should_return_an_ArgumentException()
		{
			var test = new TestObject { Value = 1 };

			"test".IfNot(() => test.Value == 1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IfNot_should_also_return_an_ArgumentException()
		{
			var test = new TestObject { Boolean = true };

			"test".IfNot(() => test.Boolean && test.Boolean);
		}

		[Test]
		public void ValueOrDefault_should_return_null_if_the_root_is_null()
		{
			const TestObject nullTest = null;

			nullTest.ValueOrDefault(t => t.Child).ShouldBeNull();
		}

		[Test]
		public void ValueOrDefault_should_return_null_if_the_expression_results_in_null()
		{
			var test = new TestObject { Child = new TestObject() };

			test.ValueOrDefault(t => t.Child.Child).ShouldBeNull();
		}

		[Test]
		public void ValueOrDefault_should_return_the_result_of_the_expression_if_there_are_no_nulls()
		{
			var test = new TestObject { Child = new TestObject { Child = new TestObject() } };

			test.ValueOrDefault(t => t.Child.Child).ShouldNotBeNull();
		}

		[Test]
		public void ValueOrDefault_should_return_the_default_value_of_the_type_if_the_return_type_is_not_nullable_and_there_value_could_not_be_retrieved()
		{
			var test = new TestObject { Child = new TestObject() };

			test.ValueOrDefault(t => t.Child.Child.Value).ShouldEqual(0);
		}

		public class TestObject
		{
			public TestObject Child { get; set; }
			public int Value { get; set; }
			public bool Boolean { get; set; }
		}
	}
}