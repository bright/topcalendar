using TopCalendar.Utility.UI;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows.Input;
using TopCalendar.UI.Infrastructure;
using Microsoft.Practices.Composite.Events;
using TopCalendar.UI.PluginManager;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System;
using Microsoft.Practices.ServiceLocation;
using System.Windows;
using TopCalendar.UI.Modules.Plugins.Services;

namespace TopCalendar.UI.Modules.Plugins
{
	public class PluginsViewPresentationModel
		: PresentationModelFor<IPluginsView>, IPluginsViewPresentationModel
	{
		private DelegateCommand<object> _cancelCommand;
		private DelegateCommand<object> _saveCommand;
		private DelegateCommand<PluginInfo> _removePluginCommand;
		private DelegateCommand<object> _addPluginCommand;

		private IEventAggregator _eventAggregator;
		private IPluginLoader _pluginLoader;
		private IServiceLocator _serviceLocator;

		public PluginsViewPresentationModel(IPluginsView view, IServiceLocator serviceLocator)
			: base(view)
		{
			_serviceLocator = serviceLocator;
			_eventAggregator = serviceLocator.GetInstance<IEventAggregator>();
			_pluginLoader = serviceLocator.GetInstance<IPluginLoader>();

			_cancelCommand = new DelegateCommand<object>(Cancel);
			_saveCommand = new DelegateCommand<object>(Save);
			_removePluginCommand = new DelegateCommand<PluginInfo>(RemovePluginFromList);
			_addPluginCommand = new DelegateCommand<object>(AddPluginToList);

			PluginsList = new ObservableCollection<PluginInfo>();

			InitializeList();

			_view.ViewModel = this;
		}

		private void InitializeList()
		{
			foreach (var module in _pluginLoader.ModuleCatalog.Modules)
			{
				PluginsList.Add(new PluginInfo(module));
			}
		}

		public ObservableCollection<PluginInfo> PluginsList { get; set; }

		#region Eventy

		public ICommand CancelCommand
		{
			get { return _cancelCommand; }
		}

		public ICommand SaveCommand
		{
			get { return _saveCommand; }
		}

		public ICommand RemovePluginCommand
		{
			get { return _removePluginCommand; }
		}

		public ICommand AddPluginCommand
		{
			get { return _addPluginCommand; }
		}

		#endregion
		#region Implementacje dla eventow

		private void RemovePluginFromList(PluginInfo plugin)
		{
			PluginsList.Remove(plugin);
		}

		private void AddPluginToList(object obj)
		{
			var dlg = new OpenFileDialog();
			dlg.Filter = "Pluginy TopCalendar (.dll)|*.dll";

			var result = dlg.ShowDialog();
			if (result == true)
			{
				try
				{
					PluginsList.Add(new PluginInfo(dlg.FileName));
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void Cancel(object obj)
		{
			_eventAggregator.GetEvent<DeactivateViewEvent>().Publish(View);
		}

		private void Save(object obj)
		{
			var exporter = new PluginsExporter();
			exporter.Export(PluginsList, "plugins.config");

			MessageBox.Show("Zmiany zapisane. Uruchom program ponownie\nWersja 2.0 nie bêdzie wymaga³a restartu ;)");

			_eventAggregator.GetEvent<DeactivateViewEvent>().Publish(View);
		}

		#endregion
	}
}