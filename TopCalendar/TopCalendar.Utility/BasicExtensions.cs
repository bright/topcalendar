﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TopCalendar.Utility.BasicExtensions
{
	public static class BasicExtensions
	{

		public static Exception ThrownException(this Action job)
		{
			return ThrownException<Exception>(job);
		}

		public static TException ThrownException<TException>(this Action job)
			where TException : Exception
		{			
			try
			{
				job();
			}catch(TException ex)
			{
				return ex;
			}
			return null;
		}

		public static bool IsBetween(this DateTime me, DateTime start, DateTime stop)
		{
			return me.CompareTo(start) >= 0 && me.CompareTo(stop) <= 0;
		}

		public static DateTime AtWeekStart(this DateTime date)
		{
			return date.Subtract(TimeSpan.FromDays(DayInWeek(date))).AtDayStart();
		}

		public static int DayInWeek(this DateTime date)
		{
			return ((int) date.DayOfWeek + 6)%7;
		}

		public static IEnumerable<DateTime> Range(this DateTime from,DateTime to, TimeSpan step)
		{
			var current = from;
			while(current.CompareTo(to) < 0)
			{
				yield return current;
				current = current.Add(step);
			}
			yield break;
		}

		public static DateTime AtWeekEnd(this DateTime date)
		{
			return date.AtWeekStart().AddDays(7).Subtract(TimeSpan.FromTicks(1)).AtDayEnd();
		}

		public static DateTime AtMonthStart(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime AtMonthEnd(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year,date.Month),23,59,59,999 );
		}

		public static DateTime AtDayEnd(this DateTime date)
		{
			return new DateTime(date.Year,date.Month, date.Day, 23,59,59,999);
		}

		public static DateTime AtDayStart(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day);
		}

		public static bool IsEmpty(this string stringValue)
		{
			return string.IsNullOrEmpty(stringValue);
		}

		public static bool IsNotEmpty(this string stringValue)
		{
			return !string.IsNullOrEmpty(stringValue);
		}

		public static bool ToBool(this string stringValue)
		{
			if (string.IsNullOrEmpty(stringValue)) return false;

			return bool.Parse(stringValue);
		}

		public static string ToFormat(this string stringFormat, params object[] args)
		{
			return String.Format(stringFormat, args);
		}

		public static string If(this string html, Expression<Func<bool>> modelBooleanValue)
		{
			return GetBooleanPropertyValue(modelBooleanValue) ? html : string.Empty;
		}

		public static string IfNot(this string html, Expression<Func<bool>> modelBooleanValue)
		{
			return !GetBooleanPropertyValue(modelBooleanValue) ? html : string.Empty;
		}

		private static bool GetBooleanPropertyValue(Expression<Func<bool>> modelBooleanValue)
		{
			var prop = modelBooleanValue.Body as MemberExpression;
			if (prop != null)
			{
				var info = prop.Member as PropertyInfo;
				if (info != null)
				{
					return modelBooleanValue.Compile().Invoke();
				}
			}
			throw new ArgumentException("The modelBooleanValue parameter should be a single property, validation logic is not allowed, only 'x => x.BooleanValue' usage is allowed, if more is needed do that in the Controller");
		}
/*
		public static string ToFullUrl(this string relativeUrl, params object[] args)
		{
			var formattedUrl = (args == null) ? relativeUrl : relativeUrl.ToFormat(args);

			return UrlContext.GetFullUrl(formattedUrl);
		}
*/
		public static VALUE Get<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, KEY key)
		{
			return dictionary.Get(key, default(VALUE));
		}

		public static VALUE Get<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, KEY key, VALUE defaultValue)
		{
			if (dictionary.ContainsKey(key)) return dictionary[key];
			return defaultValue;
		}

		// TODO: Not used and seems not wanted anyway
		public static string GetViewModelProperty<VIEWMODEL>(this IDictionary<string, object> dictionary, Expression<Func<VIEWMODEL, object>> expression)
		{
			string key = ReflectionHelper.GetProperty(expression).Name;
			if (dictionary.ContainsKey(key)) return dictionary[key].ToString();
			return string.Empty;
		}

		public static bool Exists<T>(this IEnumerable<T> values, Func<T, bool> evaluator)
		{
			return values.Count(evaluator) > 0;
		}

		[DebuggerStepThrough]
		public static IEnumerable<T> Each<T>(this IEnumerable<T> values, Action<T> eachAction)
		{
			foreach (var item in values)
			{
				eachAction(item);
			}

			return values;
		}

		[DebuggerStepThrough]
		public static IEnumerable Each(this IEnumerable values, Action<object> eachAction)
		{
			foreach (var item in values)
			{
				eachAction(item);
			}

			return values;
		}

		[DebuggerStepThrough]
		public static int IterateFromZero(this int maxCount, Action<int> eachAction)
		{
			for (var idx = 0; idx < maxCount; idx++)
			{
				eachAction(idx);
			}

			return maxCount;
		}

		public static bool HasCustomAttribute<ATTRIBUTE>(this MemberInfo member)
			where ATTRIBUTE : Attribute
		{
			return member.GetCustomAttributes(typeof(ATTRIBUTE), false).Any();
		}

		public static bool IsNullable(this Type theType)
		{
			return (!theType.IsValueType) || theType.IsNullableOfT();
		}

		public static bool IsNullableOfT(this Type theType)
		{
			return theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
		}

		public static bool IsNullableOf(this Type theType, Type otherType)
		{
			return theType.IsNullableOfT() && theType.GetGenericArguments()[0].Equals(otherType);
		}

		public static IList<T> AddMany<T>(this IList<T> list, params T[] items)
		{
			return list.AddRange(items);
		}

		public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> items)
		{
			items.Each(t => list.Add(t));
			return list;
		}

		public static U ValueOrDefault<T, U>(this T root, Expression<Func<T, U>> expression)
			where T : class
		{
			if (root == null)
			{
				return default(U);
			}

			var accessor = ReflectionHelper.GetAccessor(expression);

			object result = accessor.GetValue(root);

			return (U)(result ?? default(U));
		}
	}
}