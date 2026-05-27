using System.Windows.Input;

namespace TaskManager.Pages;

public partial class ShowLists : ContentPage
{
	public ShowLists()
	{
		InitializeComponent();

		BindingContext = this;
	}

    /// <summary>
    /// Bring the user to the corresponding list's tasks
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnListTapped(object sender, TappedEventArgs e)
    {
        // TO FIX, NOT WORKING PROPERLY. NEED TO STOP THE HARD CODED IDs
        int? id = Convert.ToInt32(e.Parameter?.ToString());

        await Shell.Current.GoToAsync($"{nameof(ShowTasks)}?listId={id}", true);
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
}