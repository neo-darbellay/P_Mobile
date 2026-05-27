namespace TaskManager.Pages;

public partial class NewTask : ContentPage
{
	public NewTask()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Undo the task creation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
    }

    /// <summary>
    /// Create the new task, making sure that all required fields are filled in
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Create_Clicked(object sender, EventArgs e)
    {

    }
}