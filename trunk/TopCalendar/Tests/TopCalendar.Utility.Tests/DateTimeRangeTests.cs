using System;
using NUnit.Framework;

namespace TopCalendar.Utility.Tests
{
	[TestFixture]
	public abstract class observations_for_date_in_range
		: observations_for_sut_of_type<DateTimeRange>
	{
		protected bool _isInRange;	
		protected DateTime _stop;
		protected DateTime _start;
				
		protected override DateTimeRange CreateSut()
		{
			return new DateTimeRange(_start,_stop);
		}

		protected override void EstablishContext()
		{
			_start = new DateTime(1999,10,2,10,0,0);
			_stop = new DateTime(1999,10,2,10,0,1);			
		}		
	}

	public class when_date_is_in_range : observations_for_date_in_range
	{
		private DateTime _dateInRange;

		protected override void Because()
		{
			_isInRange = Sut.IsBetween(_dateInRange);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_dateInRange = new DateTime(1999, 10, 2, 10, 0, 1);			
		}

		[Test]
		public void should_return_true()
		{
			_isInRange.ShouldBeTrue();
		}
	}

	public class when_date_is_not_in_range : observations_for_date_in_range
	{
		private DateTime _dateNotInRange;

		protected override void Because()
		{
			_isInRange = Sut.IsBetween(_dateNotInRange);
		}

		protected override void EstablishContext()
		{
			base.EstablishContext();
			_dateNotInRange = new DateTime(1999, 10, 2, 10, 0, 4);
		}

		[Test]
		public void should_return_false()
		{
			_isInRange.ShouldBeFalse();
		}
	}
}