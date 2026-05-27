using CommunityToolkit.Maui.Views;
using System.Threading.Tasks;
using TaskManager.Components;

namespace TaskManager.Pages;

public partial class NewList : ContentPage
{
	/// <summary>
	/// Variable used to stock the user's chosen color
	/// </summary>
	private Color _userPickedColor;

	/// <summary>
	/// The list's color, chosen by the user
	/// </summary>
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

    /// <summary>
    /// Open a pop-up to allow the user to choose a color
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OpenColorPopup(object sender, TappedEventArgs e)
    {
        // Create a new popup
        ColorPickerPopup popup = new(UserPickedColor);
        Object? result = await this.ShowPopupAsync(popup);

        // Verify that the result is a color, then update the chosen color
        if (result is Color color)
            UserPickedColor = color;
    }

    /// <summary>
    /// Undo the list creation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Cancel_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..", true);
    }

    /// <summary>
    /// Create the new list, making sure that all required fields are filled in
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Create_Clicked(object sender, EventArgs e)
    {

    }
}