using System.Windows.Input;
using TaskManager.Services;

namespace TaskManager.Pages;

public partial class ShowLists : ContentPage
{
    /// <summary>
    /// A Task Item Service that will be sent to lists, used for Task Item CRUD
    /// </summary>
    private readonly TaskItemService _taskItemService;

	public ShowLists()
	{
		InitializeComponent();

        _taskItemService = new TaskItemService();

		BindingContext = this;
	}

    /// <summary>
    /// Bring the user to the corresponding list's tasks
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnListTapped(object sender, TappedEventArgs e)
    {
        // TO FIX, NEED TO FIND A WAY TO STOCK THE LIST ID INSIDE OF THE CODE ITSELF IF WE WANT TO MAKE THE CRUD FOR LIST
        int? id = Convert.ToInt32(e.Parameter?.ToString());

        Dictionary<string, object> navigationParameter = new()
        {
            { "listId", id },
            { "taskItemService", _taskItemService },
        };

        await Shell.Current.GoToAsync(nameof(ShowTasks), true, navigationParameter);
    }

    /// <summary>
    /// Bring the user to the tags page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnTagsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ShowTags), true);
    }

    /// <summary>
    /// Send the user to the list creation page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnAddTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewList), true);
    }
}