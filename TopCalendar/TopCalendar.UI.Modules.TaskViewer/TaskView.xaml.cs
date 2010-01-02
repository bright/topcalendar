using System;

namespace TopCalendar.UI.Modules.TaskViewer
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : ITaskView
    {
        public TaskView()
        {
            InitializeComponent();
        }

        public TaskPresentationModel ViewModel
        {
            get { return (TaskPresentationModel)DataContext; }
            set { DataContext = value; }
        }

    	private bool _isActive;

    	public bool IsActive
    	{
    		get { return _isActive; }
    		set { _isActive = value;
    			RaiseIsActiveChanged();
			}
    	}

    	private void RaiseIsActiveChanged()
    	{
    		if(IsActiveChanged != null)
    		{
				IsActiveChanged(this, new EventArgs());
    		}
    	}

    	public event EventHandler IsActiveChanged;
    }
 
}
