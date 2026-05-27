using System.Collections.ObjectModel;
using TaskManager.Models;

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
            _listId = Uri.UnescapeDataString(value);
            LoadData();
        }
    }

    // Propriété bindée au Label (Description)
    private string _listDescription;
    public string Description
    {
        get => _listDescription;
        set
        {
            _listDescription = value;
            OnPropertyChanged();
        }
    }

    // Collections pour les listes
    public ObservableCollection<TaskItem> TasksTodo { get; set; } = new();
    public ObservableCollection<TaskItem> TasksDone { get; set; } = new();

    public ShowTasks()
    {
        InitializeComponent();
        BindingContext = this;

        _listId = string.Empty;
        _listDescription = string.Empty;

        Title = "Chargement...";
    }

    private void LoadData()
    {
        // Mise à jour Title (binding OK)
        Title = $"Tâches \"{ListId}\"";

        // Description dynamique
        Description = $"Tâches à faire à la maison";

        // Exemple de données
        TasksTodo.Clear();
        TasksDone.Clear();

        TasksTodo.Add(new TaskItem
        {
            Title = "Prendre mes médicaments",
            Description = "Pour les allergies",
            Done = false,
        });

        TasksTodo.Add(new TaskItem
        {
            Title = "Nourrir le poisson rouge",
            Description = "Nourriture bio",
            Done = false,
        });

        TasksDone.Add(new TaskItem
        {
            Title = "Acheter du pain",
            Description = "Un pain paysan",
            Done = true
        });

        System.Diagnostics.Debug.WriteLine($"List ID: {ListId}");
    }

    public void MarkTaskAsDone(TaskItem task)
    {
        if (TasksTodo.Contains(task))
        {
            TasksTodo.Remove(task);
            task.Done = true;
            TasksDone.Add(task);
        }
    }

    public void MarkTaskAsTodo(TaskItem task)
    {
        if (TasksDone.Contains(task))
        {
            TasksDone.Remove(task);
            task.Done = false;
            TasksTodo.Add(task);
        }
    }


    private void OnTaskCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is TaskItem task)
        {
            var vm = BindingContext as ShowTasks;
            if (vm == null) return;

            if (e.Value)
            {
                vm.MarkTaskAsDone(task);
            }
            else
            {
                vm.MarkTaskAsTodo(task);
            }
        }
    }

    private void OnAddTapped (object sender, TappedEventArgs e)
    {

    }
}