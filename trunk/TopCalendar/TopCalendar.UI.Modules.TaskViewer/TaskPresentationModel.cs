using System;
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

        #region Constructors

        public TaskPresentationModel(ITaskView view, IEventAggregator eventAggregator, 
            ITaskRepository taskRepository)
            : base(view)
        {
            _eventAggregator = eventAggregator;
            _taskRepository = taskRepository;
            _view.ViewModel = this;


            _task = new Task();
            _cancelCommand = new DelegateCommand<object>(Cancel);
            _updateCommand = new DelegateCommand<object>(Update, CanUpdate);
            _addCommand = new DelegateCommand<object>(Add, CanAdd);

        }

        #endregion

        #region interface ITaskPresentationModel

        public void ShowAddNewTaskView(DateTime newDateTime)
        {
            _task = new Task();
            _task.Description = string.Empty;
            if (newDateTime.Equals(new DateTime()))
                newDateTime = DateTime.Now;

            _task.StartAt = newDateTime;

            Description = _task.Description;
            Name = _task.Name;
            StartAt = _task.StartAt;
        }

        #endregion

        #region private variables

        private readonly IEventAggregator _eventAggregator;
        private readonly ITaskRepository _taskRepository;
        private Task _task;

        #endregion

        #region Properies


        [StringLengthValidator(0, 200)]
        public string Description
        {
            get { return _task.Description; }
            set
            {
                _task.Description = value;
                OnPropertyChanged("Description");
                _updateCommand.RaiseCanExecuteChanged();
                _addCommand.RaiseCanExecuteChanged();
            }
        }

        [StringLengthValidator(3, 40)]
        public string Name
        {
            get { return _task.Name; }
            set
            {
                _task.Name = value;
                OnPropertyChanged("Name");
                _updateCommand.RaiseCanExecuteChanged();
                _addCommand.RaiseCanExecuteChanged();
            }
        }

        //[DateTimeRangeValidator("1960-01-01T00:00:00")]
        public DateTime StartAt
        {
            get { return _task.StartAt; }
            set
            {
                _task.StartAt = value;
                OnPropertyChanged("StartAt");
                _updateCommand.RaiseCanExecuteChanged();
                _addCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime EndAt
        {
            get { return _task.StartAt; }
            set
            {
                _task.StartAt = value;
                OnPropertyChanged("EndAt");
                _updateCommand.RaiseCanExecuteChanged();
                _addCommand.RaiseCanExecuteChanged();
            }
        }

        [Inject]
        public ILoggerFacade Log { get; set; }

        #endregion

        #region Commands

        private DelegateCommand<object> _cancelCommand;
        private DelegateCommand<object> _updateCommand;
        private DelegateCommand<object> _addCommand;

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
                _updateCommand = (DelegateCommand<object>)value;
                OnPropertyChanged("UpdateCommand");
            }
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set
            {
                _addCommand = (DelegateCommand<object>)value;
                OnPropertyChanged("AddCommand");
            }
        }

        #endregion

        #region private methods

        

        private void Cancel(object obj)
        {
            _eventAggregator.GetEvent<UnloadViewEvent>().Publish(View);
            Log.Log("Operacja na zadaniu anulowana", Category.Info, Priority.None);
        }

        private void Update(object obj)
        {
            bool success = _taskRepository.UpdateTask(_task);
            if (success)
            {
                Cancel(null);
            }
        }

        private void Add(object obj)
        {
            bool success = _taskRepository.AddTask(_task);

            if (success)
                Cancel(null);
        }

        private bool CanAdd(object arg)
        {
            return CanUpdate(null);
        }
        private bool CanUpdate(object arg)
        {
            bool isValid = Validation.Validate(this).IsValid;
            return isValid;
        }

        #endregion

    }
}
