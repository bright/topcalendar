using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Ninject;
using TopCalendar.Client.Connector;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.UI;


namespace TopCalendar.UI.Modules.TaskViewer
{
    public class TaskPresentationModel : PresentationModelFor<ITaskView>, ITaskPresentationModel
    {
        public TaskPresentationModel(ITaskView view, IEventAggregator eventAggregator, 
            ITaskRepository taskRepository)
            : base(view)
        {
            _eventAggregator = eventAggregator;
            _taskRepository = taskRepository;            
            _cancelCommand = new DelegateCommand<object>(Cancel);
            _updateCommand = new DelegateCommand<Task>(Update, CanUpdate);
			_addCommand = new DelegateCommand<Task>(Add, CanAdd);
			_view.ViewModel = this;        	
        }

   
    

    	public void ShowAddNewTaskView(DateTime? newDateTime)
        {
        	IsNewTask = true;
            Task = new Task("Nazwa", newDateTime ?? DateTime.Now);            
        }

    	public bool IsNewTask
    	{    		
			get
			{
				return AddButtonVisible;
			}
    		set
    		{
    			UpdateButtonVisible = !value;
    			AddButtonVisible = value;
    		}
    	}

    	private readonly IEventAggregator _eventAggregator;
        private readonly ITaskRepository _taskRepository;
        

        
		private Task _task;
    	public Task Task
    	{
			get { return _task; }
			set {
				SetTask(value);
			}
    	}

    	private void SetTask(Task value)
    	{
    		if(_task != null)
    			UnBindCanExecuteCommands();
    		_task = value;
    		BindCanExecuteCommands();
    		OnPropertyChanged(()=>Task);
    	}


    	[Inject]
        public ILoggerFacade Log { get; set; }

    

        private DelegateCommand<object> _cancelCommand;
		private DelegateCommand<Task> _updateCommand;
		private DelegateCommand<Task> _addCommand;
    	

    	public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                _cancelCommand = (DelegateCommand<object>)value;
                OnPropertyChanged("CancelCommand");
            }
        }

        public ICommand UpdateCommand
        {
            get { return _updateCommand; }
            set
            {
				_updateCommand = (DelegateCommand<Task>)value;
                OnPropertyChanged("UpdateCommand");
            }
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set
            {
				_addCommand = (DelegateCommand<Task>)value;
                OnPropertyChanged("AddCommand");
            }
        }

    	private bool _addButtonVisible;
    	public bool AddButtonVisible
    	{
    		get { return _addButtonVisible; }
    		set { _addButtonVisible = value; 
				OnPropertyChanged(()=> AddButtonVisible);
			}
    	}

    	private bool _updateButtonVisible;
    	public bool UpdateButtonVisible
    	{
    		get { return _updateButtonVisible; }
    		set { _updateButtonVisible = value; 
				OnPropertyChanged(()=> UpdateButtonVisible);
			}
    	}

    	private void Cancel(object obj)
        {
            _eventAggregator.GetEvent<UnloadViewEvent>().Publish(View);
            Log.Log("Operacja na zadaniu anulowana", Category.Info, Priority.None);
        }

        private void Update(Task task)
        {
			bool success = _taskRepository.UpdateTask(task);
            if (success)
            {
                Cancel(null);
            }
        }

        private void Add(Task task)
        {
			bool success = _taskRepository.AddTask(task);

            if (success)
                Cancel(null);
        }

        private bool CanAdd(Task task)
        {
			return CanUpdate(task);
        }
        private bool CanUpdate(Task task)
        {
			bool isValid = Validation.Validate(task).IsValid;
            return isValid;
        }

		private void BindCanExecuteCommands()
		{
			_task.PropertyChanged += RaiseCommandCanExecuteChanged;
		}

		private void RaiseCommandCanExecuteChanged(object sender, PropertyChangedEventArgs args)
		{
			_addCommand.RaiseCanExecuteChanged();
			_updateCommand.RaiseCanExecuteChanged();
		}


		private void UnBindCanExecuteCommands()
		{
			_task.PropertyChanged -= RaiseCommandCanExecuteChanged;
		}
    }
}
