namespace TaskManager.Pages;

public partial class NewTask : ContentPage
{
	public NewTask()
	{
		InitializeComponent();
	}

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
    }

    private void Create_Clicked(object sender, EventArgs e)
    {

    }
}