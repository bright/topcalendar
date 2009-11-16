using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace TopCalendar.Utility.UI
{
	public abstract class NotifyPropertyChanged : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{

			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected void OnPropertyChanged<TPropertyType>(Expression<Func<TPropertyType>> expression)
		{
			var me = expression.Body as MemberExpression;
			if (me != null) OnPropertyChanged(me.Member.Name);
		}
	}
}