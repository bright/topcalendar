using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TopCalendar.Utility
{
	public static class ReflectionHelper
	{
		public static bool MeetsSpecialGenericConstraints(Type genericArgType, Type proposedSpecificType)
		{
			var gpa = genericArgType.GenericParameterAttributes;
			var constraints = gpa & GenericParameterAttributes.SpecialConstraintMask;

			// No constraints, away we go!
			if (constraints == GenericParameterAttributes.None)
				return true;

			// "class" constraint and this is a value type
			if ((constraints & GenericParameterAttributes.ReferenceTypeConstraint) != 0
			    && proposedSpecificType.IsValueType)
			{
				return false;
			}

			// "struct" constraint and this is a value type
			if ((constraints & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0
			    && !proposedSpecificType.IsValueType)
			{
				return false;
			}

			// "new()" constraint and this type has no default constructor
			if ((constraints & GenericParameterAttributes.DefaultConstructorConstraint) != 0
			    && proposedSpecificType.GetConstructor(Type.EmptyTypes) == null)
			{
				return false;
			}

			return true;
		}

		public static PropertyInfo GetProperty<MODEL>(Expression<Func<MODEL, object>> expression)
		{
			MemberExpression memberExpression = getMemberExpression(expression);
			return (PropertyInfo)memberExpression.Member;
		}

		public static PropertyInfo GetProperty<MODEL, T>(Expression<Func<MODEL, T>> expression)
		{
			MemberExpression memberExpression = getMemberExpression(expression);
			return (PropertyInfo)memberExpression.Member;
		}

		private static MemberExpression getMemberExpression<MODEL, T>(Expression<Func<MODEL, T>> expression)
		{
			MemberExpression memberExpression = null;
			if (expression.Body.NodeType == ExpressionType.Convert)
			{
				var body = (UnaryExpression)expression.Body;
				memberExpression = body.Operand as MemberExpression;
			}
			else if (expression.Body.NodeType == ExpressionType.MemberAccess)
			{
				memberExpression = expression.Body as MemberExpression;
			}


			if (memberExpression == null) throw new ArgumentException("Not a member access", "member");
			return memberExpression;
		}

		public static Accessor GetAccessor<MODEL>(Expression<Func<MODEL, object>> expression)
		{
			MemberExpression memberExpression = getMemberExpression(expression);

			return GetAccessor(memberExpression);
		}

		public static Accessor GetAccessor(MemberExpression memberExpression)
		{
			var list = new List<PropertyInfo>();

			while (memberExpression != null)
			{
				list.Add((PropertyInfo)memberExpression.Member);
				memberExpression = memberExpression.Expression as MemberExpression;
			}

			if (list.Count == 1)
			{
				return new SingleProperty(list[0]);
			}

			list.Reverse();
			return new PropertyChain(list.ToArray());
		}

		public static Accessor GetAccessor<MODEL, T>(Expression<Func<MODEL, T>> expression)
		{
			MemberExpression memberExpression = getMemberExpression(expression);

			return GetAccessor(memberExpression);
		}

		public static MethodInfo GetMethod<T>(Expression<Func<T, object>> expression)
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}

		public static MethodInfo GetMethod<DELEGATE>(Expression<DELEGATE> expression)
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}

		public static MethodInfo GetMethod<T, U>(Expression<Func<T, U>> expression)
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}

		public static MethodInfo GetMethod<T, U, V>(Expression<Func<T, U, V>> expression)
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}

		public static MethodInfo GetMethod<T>(Expression<Action<T>> expression)
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}


		public static T GetAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			object[] atts = provider.GetCustomAttributes(typeof(T), true);
			return atts.Length > 0 ? atts[0] as T : null;
		}

		public static void ForAttribute<T>(this ICustomAttributeProvider provider, Action<T> action) where T : Attribute
		{
			foreach (T attribute in provider.GetCustomAttributes(typeof(T), true))
			{
				action(attribute);
			}
		}

		public static void ForAttribute<T>(this Accessor accessor, Action<T> action) where T : Attribute
		{
			foreach (T attribute in accessor.InnerProperty.GetCustomAttributes(typeof(T), true))
			{
				action(attribute);
			}
		}
	}

	public interface Accessor
	{
		string FieldName { get; }

		Type PropertyType { get; }
		PropertyInfo InnerProperty { get; }
		Type DeclaringType { get; }
		string Name { get; }
		void SetValue(object target, object propertyValue);
		object GetValue(object target);
		Type OwnerType { get; }

		Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression);
	}

	public class SingleProperty : Accessor
	{
		private readonly PropertyInfo _property;

		public SingleProperty(PropertyInfo property)
		{
			_property = property;
		}

		#region Accessor Members

		public string FieldName
		{
			get { return _property.Name; }
		}

		public Type PropertyType
		{
			get { return _property.PropertyType; }
		}

		public Type DeclaringType
		{
			get { return _property.DeclaringType; }
		}


		public PropertyInfo InnerProperty
		{
			get { return _property; }
		}

		public Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.GetProperty(expression);
			return new PropertyChain(new[] { _property, property });
		}

		public string Name
		{
			get { return _property.Name; }
		}

		public void SetValue(object target, object propertyValue)
		{
			if (_property.CanWrite)
			{
				_property.SetValue(target, propertyValue, null);
			}
		}

		public object GetValue(object target)
		{
			return _property.GetValue(target, null);
		}

		public Type OwnerType
		{
			get { return DeclaringType; }
		}

		#endregion

		public static SingleProperty Build<T>(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.GetProperty(expression);
			return new SingleProperty(property);
		}

		public static SingleProperty Build<T>(string propertyName)
		{
			PropertyInfo property = typeof(T).GetProperty(propertyName);
			return new SingleProperty(property);
		}
	}

	public class PropertyChain : Accessor
	{
		private readonly PropertyInfo[] _chain;
		private readonly SingleProperty _innerProperty;


		public PropertyChain(PropertyInfo[] properties)
		{
			_chain = new PropertyInfo[properties.Length - 1];
			for (int i = 0; i < _chain.Length; i++)
			{
				_chain[i] = properties[i];
			}

			_innerProperty = new SingleProperty(properties[properties.Length - 1]);
		}

		#region Accessor Members

		public void SetValue(object target, object propertyValue)
		{
			target = findInnerMostTarget(target);
			if (target == null)
			{
				return;
			}

			_innerProperty.SetValue(target, propertyValue);
		}

		public object GetValue(object target)
		{
			target = findInnerMostTarget(target);

			if (target == null)
			{
				return null;
			}

			return _innerProperty.GetValue(target);
		}

		public Type OwnerType
		{
			get { return _chain.Last().PropertyType; }
		}

		public string FieldName
		{
			get { return _innerProperty.FieldName; }
		}

		public Type PropertyType
		{
			get { return _innerProperty.PropertyType; }
		}

		public PropertyInfo InnerProperty
		{
			get { return _innerProperty.InnerProperty; }
		}

		public Type DeclaringType
		{
			get { return _chain[0].DeclaringType; }
		}

		public Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.GetProperty(expression);
			var list = new List<PropertyInfo>(_chain);
			list.Add(_innerProperty.InnerProperty);
			list.Add(property);

			return new PropertyChain(list.ToArray());
		}

		public string Name
		{
			get
			{
				string returnValue = string.Empty;
				foreach (PropertyInfo info in _chain)
				{
					returnValue += info.Name;
				}

				returnValue += _innerProperty.Name;

				return returnValue;
			}
		}

		#endregion

		private object findInnerMostTarget(object target)
		{
			foreach (PropertyInfo info in _chain)
			{
				target = info.GetValue(target, null);
				if (target == null)
				{
					return null;
				}
			}

			return target;
		}
	}
}