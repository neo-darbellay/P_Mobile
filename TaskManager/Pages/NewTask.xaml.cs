using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Pages;

public partial class NewTask : ContentPage, IQueryAttributable
{
    /// <summary>
    /// The task we are editing
    /// </summary>
    private TaskItem _task;
    /// <summary>
    /// The service used to save
    /// </summary>
    private TaskItemService _taskService;
    /// <summary>
    /// Determine if we are editing or creating
    /// </summary>
    private bool _isEditing; 

	public NewTask()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Get the navigation parameters
    /// </summary>
    /// <param name="query"></param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //GET the task to edit
        if (query.TryGetValue("task", out object? taskObj) && taskObj is TaskItem task)
        {
            _task = task;
            //since the title is not-nullable, we determine wether we create if it is empty
            _isEditing = !(string.IsNullOrEmpty(task.Title));

            //set the fields
            TitleEntry.Text = _task.Title;
            DescriptionEntry.Text = _task.Description;
        }
        //GET the taskService
        if (query.TryGetValue("dataService", out object? taskServiceObj) && taskServiceObj is TaskItemService taskService)
        {
            _taskService = taskService;
        }
    }

    /// <summary>
    /// Undo the task creation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
    }

    /// <summary>
    /// Create the new task, making sure that all required fields are filled in
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnCreateClicked(object sender, EventArgs e)
    {
        string taskTitle = TitleEntry.Text;
        string taskDescription = DescriptionEntry.Text;

        //si tasktitle est null, on ne crée rien
        if (string.IsNullOrWhiteSpace(taskTitle))
        {
            return;
        }

        //set the task's value
        _task.Title = taskTitle;
        _task.Description = taskDescription;
        //set a new id if we are creating
        if (!_isEditing)
        {
            _task.Id = await _taskService.GetNextId();
        }

        if (_isEditing)
        {
            //save an existing task
            await _taskService.SaveTaskAsync(_task);
        }
        else
        {
            //save a brand new task
            await _taskService.CreateTaskAsync(_task);
        }

        await Shell.Current.GoToAsync("ShowTasks", true);
    }
}