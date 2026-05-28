using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Pages;

[QueryProperty(nameof(ListId), "listId")]
public partial class ShowTasks : ContentPage
{
    private string _listId;

    public string ListId
    {
        get => _listId;
        set
        {
            _listId = Uri.UnescapeDataString(value);
            LoadData();
        }
    }

    /// <summary>
    /// Whether or not the device is shaking
    /// </summary>
    private bool _isShakeProcessing = false;

    /// <summary>
    /// The list's name - to change later
    /// </summary>
    private string _listName = "Maison";
    private string _listDescription = "Tâches à faire à la maison";

    /// <summary>
    /// A list of task that are done here
    /// </summary>
    public ObservableCollection<TaskItem> _tasksTodo { get; set; } = new();
    /// <summary>
    /// A list of task that aren't done here
    /// </summary>
    public ObservableCollection<TaskItem> _tasksDone { get; set; } = new();
    /// <summary>
    /// The dataservice that goes and uses the JSON
    /// </summary>
    private TaskItemService _taskService;
    /// <summary>
    /// All tasks in the list
    /// </summary>
    private ObservableCollection<TaskItem> _tasks;

    public ShowTasks()
    {
        InitializeComponent();
        BindingContext = this;

        _listId = string.Empty;
        Title = "Chargement...";

        //initialize a new service
        _taskService = new TaskItemService();

        //link the data with the app
        _tasksTodo = new ObservableCollection<TaskItem>();
        _tasksDone = new ObservableCollection<TaskItem>();

        TasksTodo.ItemsSource = _tasksTodo;
        TasksDone.ItemsSource = _tasksDone;

        ListTitle.Text = $"Tâches {_listName}";
        ListDescription.Text = _listDescription;

        // Add the accelerometer shake detection
        Accelerometer.Default.ShakeDetected += OnShakeDetected;

        if (!Accelerometer.Default.IsMonitoring)
        {
            Accelerometer.Default.Start(SensorSpeed.UI);
        }
    }

    /// <summary>
    /// Loads and set all items needed
    /// </summary>
    /// <returns></returns>
    private async Task LoadData()
    {
        // page's title - hard coded
        Title = $"Tâches \"{ListId}\"";

        //load all tasks
        _tasks = new ObservableCollection<TaskItem>(await _taskService.LoadTasksAsync());
    }

    /// <summary>
    /// Navigate to the form page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    private async void OnAddTapped(object sender, EventArgs e)
    {
        //navigate to the form page
        Dictionary<string, object> navigationParameter = new Dictionary<string, object>
            {
                { "task", new TaskItem() },
                { "tasks", _tasks },
                { "dataService", _taskService }
            };
        await Shell.Current.GoToAsync("NewTask", navigationParameter);
    }

    /// <summary>
    /// Returns to the homepage
    /// </summary>
    private async void OnHomeTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Home", true);
    }

    /// <summary>
    /// Triggered when the device is shaken
    /// </summary>
    private async void OnShakeDetected(object sender, EventArgs e)
    {
        // Prevent spam shakes
        if (_isShakeProcessing)
            return;

        _isShakeProcessing = true;

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            Dictionary<string, object> navigationParameter = new Dictionary<string, object>
            {
                { "task", new TaskItem() },
                { "tasks", _tasks },
                { "dataService", _taskService }
            };

            // Navigate to NewTask
            await Shell.Current.GoToAsync("NewTask", navigationParameter);

            // Wait 2 seconds before allowing another shake
            await Task.Delay(2000);

            _isShakeProcessing = false;
        });
    }

    /// <summary>
    /// Clean up events
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Cleanup
        Accelerometer.Default.ShakeDetected -= OnShakeDetected;

        if (Accelerometer.Default.IsMonitoring)
        {
            Accelerometer.Default.Stop();
        }
    }

    /// <summary>
    /// Refresh the data manually
    /// </summary>
    protected override async void OnAppearing()
        {
        base.OnAppearing();

        _tasks = new ObservableCollection<TaskItem>(await _taskService.LoadTasksAsync());
        //reset the 2 lists if not null
        if (_tasks is ObservableCollection<TaskItem>)
        {
            _tasksDone.Clear();
            _tasksDone = new ObservableCollection<TaskItem>(_tasks.ToList().FindAll(t => t.Done == true));
            TasksDone.ItemsSource = _tasksDone;

            _tasksTodo.Clear();
            _tasksTodo = new ObservableCollection<TaskItem>(_tasks.ToList().FindAll(t => t.Done == false));
            TasksTodo.ItemsSource = _tasksTodo;
        }
    }

    /// <summary>
    /// Delete a task
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        //get the task's id
        Border? border = sender as Border;
        TaskItem? task = border.BindingContext as TaskItem;
        int taskId = -1;
        taskId = task.Id;

        //if null, we abort early
        if (taskId == -1)
        {
            return;
        }

        //delete
        await _taskService.DeleteTaskByIdAsync(taskId);

        //force refresh the data
        OnAppearing();
    }

    private async void OnEditTapped(object sender, EventArgs e)
    {
        //get the task clicked
        VerticalStackLayout? vertical = sender as VerticalStackLayout;
        TaskItem? task = vertical.BindingContext as TaskItem;

        //if null, we abort early
        if (task == null)
        {
            return;
        }

        //navigate to the form page
        Dictionary<string, object> navigationParameter = new Dictionary<string, object>
        {
            { "task", task },
            { "tasks", _tasks },
            { "dataService", _taskService }
        };
        await Shell.Current.GoToAsync("NewTask", navigationParameter);
    }

    public void MarkTaskAsDone(TaskItem task)
    {
        if (_tasksTodo.Contains(task))
        {
            _tasksTodo.Remove(task);
            task.Done = true;
            _tasksDone.Add(task);
        }
    }

    public void MarkTaskAsTodo(TaskItem task)
    {
        if (_tasksDone.Contains(task))
        {
            _tasksDone.Remove(task);
            task.Done = false;
            _tasksTodo.Add(task);
        }
    }


    private void OnTaskCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is TaskItem task)
        {
            var vm = BindingContext as ShowTasks;
            if (vm == null) return;

            if (e.Value)
            {
                vm.MarkTaskAsDone(task);
            }
            else
            {
                vm.MarkTaskAsTodo(task);
            }
        }
    }

}