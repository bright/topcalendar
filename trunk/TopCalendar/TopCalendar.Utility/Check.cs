using System;

namespace TopCalendar.Utility
{
	public class Check
	{

		public static void Guard(bool condition, string message)
		{
			Guard<ArgumentException>(condition, message);
		}

		public static void Guard<TException>(bool condition, string message)
			where TException : Exception
		{
			try
			{
				if (condition)
					return;
			}
			catch
			{
			}
			throw (TException)Activator.CreateInstance(typeof(TException), new object[] { message });			
		}

		public static void Guard<TException>(Func<bool> condition, string message)
			where TException : Exception
		{
			Guard<TException>(condition(), message);
		}
	}
}