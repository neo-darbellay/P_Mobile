using System.Windows.Input;

namespace TaskManager.Pages;

public partial class ShowLists : ContentPage
{
	public ShowLists()
	{
		InitializeComponent();

		BindingContext = this;
	}
    private async void OnListTapped(object sender, TappedEventArgs e)
    {
        int? id = Convert.ToInt32(e.Parameter?.ToString());

        await Shell.Current.GoToAsync($"{nameof(ShowTasks)}?listId={id}", true);
    }

    private async void OnTagsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ShowTags), true);
    }

    private async void OnAddTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewList), true);
    }

    private async void OnHomeTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}