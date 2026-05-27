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

    // Propriété bindée au Label (Description)
    private string _listDescription;
    public string Description
    {
        get => _listDescription;
        set
        {
            _listDescription = value;
            OnPropertyChanged();
        }
    }

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
        _listDescription = $"Tâches à faire à la maison";

        Title = "Chargement...";

        //initialize a new service
        _taskService = new TaskItemService();

        //link the data with the app
        _tasksTodo = new ObservableCollection<TaskItem>();
        _tasksDone = new ObservableCollection<TaskItem>();

        TasksTodo.ItemsSource = _tasksTodo;
        TasksDone.ItemsSource = _tasksDone;
    }

    /// <summary>
    /// Loads and set all items needed
    /// </summary>
    /// <returns></returns>
    private async Task LoadData()
    {
        // page's title - hard coded
        Title = $"Tâches \"{ListId}\"";
        // list's description - hard coded
        ListDescription.Text = _listDescription;

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
    /// Refresh manually the data
    /// </summary>
    protected override async void OnAppearing()
        {
        base.OnAppearing();

        _tasks = new ObservableCollection<TaskItem>(await _taskService.LoadTasksAsync());
        //reset the 2 lists if not null
        if (_tasks.Count > 0)
        {
            _tasksDone.Clear();
            _tasksDone = new ObservableCollection<TaskItem>(_tasks.ToList().FindAll(t => t.Done == true));
            TasksDone.ItemsSource = _tasksDone;

            _tasksTodo.Clear();
            _tasksTodo = new ObservableCollection<TaskItem>(_tasks.ToList().FindAll(t => t.Done == false));
            TasksTodo.ItemsSource = _tasksTodo;
        }
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