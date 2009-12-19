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
    }
 
}
