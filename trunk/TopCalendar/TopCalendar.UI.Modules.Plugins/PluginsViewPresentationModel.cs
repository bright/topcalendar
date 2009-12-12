using TopCalendar.Utility.UI;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows.Input;
using TopCalendar.UI.Infrastructure;
using Microsoft.Practices.Composite.Events;
using TopCalendar.UI.PluginManager;
using System.Collections.ObjectModel;

namespace TopCalendar.UI.Modules.Plugins
{
	public class PluginsViewPresentationModel
		: PresentationModelFor<IPluginsView>, IPluginsViewPresentationModel
	{
		private DelegateCommand<object> _cancelCommand;
		private DelegateCommand<object> _saveCommand;

		private IEventAggregator _eventAggregator;
		private IPluginLoader _pluginLoader;
		private ObservableCollection<PluginInfo> _pluginsList;

		public PluginsViewPresentationModel(
			IPluginsView view, IEventAggregator eventAggregator, IPluginLoader pluginLoader
		)
			: base(view)
		{
			_eventAggregator = eventAggregator;
			_pluginLoader = pluginLoader;

			_cancelCommand = new DelegateCommand<object>(Cancel);
			_saveCommand = new DelegateCommand<object>(Save);
			_pluginsList = new ObservableCollection<PluginInfo>();

			InitializeList();

			_view.ViewModel = this;
		}

		private void InitializeList()
		{
			foreach (var module in _pluginLoader.ModuleCatalog.Modules)
			{
				_pluginsList.Add(new PluginInfo(module));
			}
		}

		public ObservableCollection<PluginInfo> PluginsList
		{
			get
			{
				return _pluginsList;
			}
			set
			{
				_pluginsList = value;
				OnPropertyChanged("PluginsList");
			}
		}

		public ICommand CancelCommand
		{
			get { return _cancelCommand; }
		}

		public ICommand SaveCommand
		{
			get { return _saveCommand; }
		}

		private void Cancel(object obj)
		{
			_eventAggregator.GetEvent<UnloadViewEvent>().Publish(View);
		}

		private void Save(object obj)
		{
			
		}
	}
}