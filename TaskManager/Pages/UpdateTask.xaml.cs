namespace TaskManager.Pages;

public partial class UpdateTask : ContentPage
{
    public UpdateTask()
    {
        InitializeComponent();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
    }

    private void Save_Clicked(object sender, EventArgs e)
    {

    }
}