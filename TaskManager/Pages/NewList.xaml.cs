using CommunityToolkit.Maui.Views;
using System.Threading.Tasks;
using TaskManager.Components;

namespace TaskManager.Pages;

public partial class NewList : ContentPage
{
	private Color _userPickedColor;
	public Color UserPickedColor
	{
		get => _userPickedColor;
		set
		{
			_userPickedColor = value;
			OnPropertyChanged();
		}
	}

	public NewList()
	{
		InitializeComponent();
		BindingContext = this;

		_userPickedColor = Colors.Red;
		UserPickedColor = Colors.Red;
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

        ColorPickerPopup popup = new(UserPickedColor);

        var result = await this.ShowPopupAsync(popup);

        if (result is Color color)
        {
            UserPickedColor = color;
        }

    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..", true);
    }

    private void Create_Clicked(object sender, EventArgs e)
    {

    }
}