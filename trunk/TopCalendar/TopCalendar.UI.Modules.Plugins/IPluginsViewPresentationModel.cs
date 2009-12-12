using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopCalendar.Utility.UI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TopCalendar.UI.Modules.Plugins
{
	public interface IPluginsViewPresentationModel : IPresentationModelFor<IPluginsView>
	{
		ObservableCollection<PluginInfo> PluginsList { get; set; }
		ICommand CancelCommand { get; }
		ICommand SaveCommand { get; }
	}
}
