using CommunityToolkit.Maui.Views;

namespace TaskManager.Components;

public partial class ColorPickerPopup : Popup
{
    /// <summary>
    /// The user's selected color
    /// </summary>
    public Color SelectedColor { get; set; }

    public ColorPickerPopup(Color initialColor)
    {
        InitializeComponent();
        SelectedColor = initialColor;
        BindingContext = this;
    }

    /// <summary>
    /// Close the popup and send the selected color through to be used
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDoneClicked(object sender, EventArgs e)
    {
        Close(SelectedColor);
    }

    /// <summary>
    /// Close the popup without sending anything to signal that the action was cancelled
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCancelClicked(object sender, EventArgs e)
    {
        Close(null);
    }
}