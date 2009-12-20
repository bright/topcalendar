using System;
using TopCalendar.Utility.Tests;

namespace TopCalendar.Server.DataLayer.Tests
{
	public abstract class observations_with_in_memory_database_of_sut_of_type<TSut> : observations_for_auto_created_sut_of_type<TSut>
		where TSut : class 
	{
		protected observations_with_in_memory_database_of_sut_of_type()
		{			
		}

		protected override void EstablishContext()
		{
			
		}		
	}
}