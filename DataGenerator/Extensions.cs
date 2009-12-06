using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using log4net;
using log4net.Core;

namespace DataGenerator.Extensions
{		

	public class DateRange
	{
		private readonly DateTime _start;
		private readonly DateTime _end;

		public DateRange(DateTime start, DateTime end)
		{
			_start = start;
			_end = end;
		}

		public DateTime Start { get { return _start; } }
		public DateTime End {get{ return _end;}}

		public IEnumerable<DateTime> MonthRange()
		{
			return new MonthRange(this);
		}
	}

	public class MonthRange : IEnumerable<DateTime>
	{
		private readonly DateRange _range;

		public MonthRange(DateRange range)
		{
			_range = range;
		}

		public IEnumerator<DateTime> GetEnumerator()
		{
			return GenerateMonthRange();
		}

		private IEnumerator<DateTime> GenerateMonthRange()
		{
			var monthStart = new DateTime(_range.Start.Year, _range.Start.Month, _range.Start.Day);
			var monthEdnt = new DateTime(_range.End.Year, _range.End.Month, _range.End.Day, 23, 59, 59);
			var result = new List<DateTime>();
			var current = monthStart;
			while(current.CompareTo(monthEdnt) <= 0)
			{
				result.Add(current);
				current = current.AddMonths(1);
			}
			return result.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	public static class StringExtensions
	{

		public static readonly Random RandomInstance = new Random((int)DateTime.Now.Ticks);

		public static int Random(this int maxValue)
		{			
			return RandomInstance.Next(maxValue);
		}

		public static int Random(this int maxValue, int minValue)
		{
			return RandomInstance.Next(minValue, maxValue);
		}
		
		public static int NextInt32(this Random rng)
		{
			unchecked
			{
				int firstBits = rng.Next(0, 1 << 4) << 28;
				int lastBits = rng.Next(0, 1 << 28);
				return firstBits | lastBits;
			}
		}

		public static decimal Random(this decimal maxValue, decimal minValue)
		{
			var nexdecimal = Math.Abs(RandomInstance.NextDecimal());
			return nexdecimal/decimal.MaxValue*(maxValue - minValue);
		}

		public static decimal NextDecimal(this Random rng)
		{
			byte scale = (byte)rng.Next(29);
			bool sign = rng.Next(2) == 1;
			return new decimal(rng.NextInt32(),
							   rng.NextInt32(),
							   rng.NextInt32(),
							   sign,
							   scale);
		}

		public static DateTime RandomDayOfMonth(this DateTime date)
		{
			return new DateTime(date.Year,date.Month,
					DateTime.DaysInMonth(date.Year,date.Month).Random(1),23.Random(),59.Random(),59.Random(),999.Random()
				);
		}

		public static string AsFormat(this string format, params object[] parameters)
		{
			return string.Format(format, parameters);
		}

		private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

		public static string LogDebug(this string message)
		{
			logger.Debug(message);
			return message;
		}

		public static IEnumerable<TType> Each<TType>(this IEnumerable<TType> collection, Action<TType> action )
		{
			Check.Require(collection != null, "Colllection cant be null");
			foreach(var item in collection)
			{
				action(item);
			}
			return collection;
		}

		public static TType Random<TType>(this IEnumerable<TType> collection)
		{
			Check.Require(collection != null, "Collection cant be null");
			var list = collection.ToList();
			Check.Require(list.Count > 0, "Collection cant be empty");
			return list[(list.Count-1).Random()];
		}
	}

	public class Log4NetWriter : TextWriter
	{		
		/// <summary>
		/// Initializes a new instance of the <see cref="Log4NetWriter"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public Log4NetWriter(Type type)
			: this(type, Level.Debug, LogManager.GetLogger(type))
		{			
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="Log4NetWriter"/> class.
		/// </summary>
		/// <param name="type">The logger source type.</param>
		/// <param name="level">The log4net log _level.</param>
		public Log4NetWriter(Type type, Level level, ILog logger)
		{
			_logSrcType = type;
			_level = level;
			_logger = logger;
			_open = true;
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.IO.TextWriter"/> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			_open = false;
			base.Dispose(disposing);
		}

		/// <summary>
		/// Writes a character to the text stream.
		/// </summary>
		/// <param name="value">The character to write to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public override void Write(char value)
		{
			if (!_open)
			{
				throw new ObjectDisposedException(null);
			}
			Log(value.ToString());
		}

		/// <summary>
		/// Writes a string to the text stream.
		/// </summary>
		/// <param name="value">The string to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public override void Write(string value)
		{
			if (!_open)
			{
				throw new ObjectDisposedException(null);
			}
			if (value != null)
			{
				Log(value);
			}
		}

		/// <summary>
		/// Writes a subarray of characters to the text stream.
		/// </summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">Starting index in the buffer.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer"/> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///     <paramref name="index"/> or <paramref name="count"/> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public override void Write(char[] buffer, int index, int count)
		{
			if (!_open)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null || index < 0 || count < 0 || buffer.Length - index < count)
			{
				base.Write(buffer, index, count); // delegate throw exception to base class
			}
			Log(new string(buffer, index, count));
		}

		/// <summary>
		/// When overridden in a derived class, returns the <see cref="T:System.Text.Encoding"/> in which the output is written.
		/// </summary>
		/// <value></value>
		/// <returns>The Encoding in which the output is written.</returns>
		public override Encoding Encoding
		{
			get
			{
				if (_encoding == null)
				{
					_encoding = new UnicodeEncoding(false, false);
				}
				return _encoding;
			}
		}


		/// <summary>
		/// Writes a message to log4net 
		/// </summary>
		/// <param name="message"></param>
		private void Log(string message)
		{
			_logger.Logger.Log(_logSrcType, _level, message, null);
		}


		private bool _open;
		private static UnicodeEncoding _encoding;
		private readonly Level _level;
		private readonly ILog _logger;
		private readonly Type _logSrcType;
	}

	public  class Check
	{
		[DebuggerStepThrough]
		public static void Require(bool b, string s)
		{
			if (!b)
				throw new ArgumentException(s);
		}
	}
}