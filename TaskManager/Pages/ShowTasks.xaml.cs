namespace TaskManager.Pages;

[QueryProperty(nameof(ListId), "listId")]
public partial class ShowTasks : ContentPage
{
    private string _listId;

    public string ListId
    {
        get => _listId;
        set
        {
            _listId = value;
            LoadData();
        }
    }

    public ShowTasks()
	{
		InitializeComponent();
	}

    private void LoadData()
    {
        TEST.Text = ListId.ToString();

        // use listId to load correct tags
        System.Diagnostics.Debug.WriteLine($"List ID: {ListId}");
    }
}