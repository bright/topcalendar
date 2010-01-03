using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Practices.ServiceLocation;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure.CommonCommands;

namespace TopCalendar.UI
{
	public class TaskToMenuItemConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var commands = ServiceLocator.Current.GetAllInstances<ILabeledCommand<Task>>();
			var result = new List<MenuItem>();
			foreach(var c in commands)
			{
				result.Add(new MenuItem
				           	{
				           		Header = c.Header,
				           		Command = c.Command,
				           		CommandParameter = value
				           	});
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}