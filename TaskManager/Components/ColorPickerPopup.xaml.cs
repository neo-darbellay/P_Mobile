using CommunityToolkit.Maui.Views;

namespace TaskManager.Components;

public partial class ColorPickerPopup : Popup
{

    public Color SelectedColor { get; set; }

    public ColorPickerPopup(Color initialColor)
    {
        InitializeComponent();
        SelectedColor = initialColor;
        BindingContext = this;
    }

    private void OnDoneClicked(object sender, EventArgs e)
    {
        Close(SelectedColor);
    }
    private void OnCancelClicked(object sender, EventArgs e)
    {
        Close(null);
    }

}