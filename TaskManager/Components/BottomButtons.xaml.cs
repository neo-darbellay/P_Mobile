using TaskManager.Pages;

namespace TaskManager.Components;

public partial class BottomButtons : ContentView
{
    public static readonly BindableProperty AddRouteProperty =
        BindableProperty.Create(
            nameof(AddRoute),
            typeof(string),
            typeof(BottomButtons),
            default(string));

    public string AddRoute
    {
        get => (string)GetValue(AddRouteProperty);
        set => SetValue(AddRouteProperty, value);
    }


    public BottomButtons()
    {
        InitializeComponent();
    }

    private async void OnHomeTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnAddTapped(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(AddRoute)) return;

        await Shell.Current.GoToAsync(AddRoute);
    }
}