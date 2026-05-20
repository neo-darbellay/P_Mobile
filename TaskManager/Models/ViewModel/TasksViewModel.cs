using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.data;

namespace TaskManager.Models.ViewModel
{
    public partial class TasksViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;

        public TasksViewModel(DatabaseContext context)
        {
            _context = context;
        }

        [ObservableProperty]
        private ObservableCollection<TaskManager.Models.Task> _tasks = new ObservableCollection<Task>();

        [ObservableProperty]
        private TaskManager.Models.Task _operatingTask = new TaskManager.Models.Task();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busytext;

        /// <summary>
        /// Ne fonctionne pas, le code delete et réinsère la même task... / crash potentiel
        /// Ceci car je n'ai pas accès aux champs en bas de page depuis ici (et ce ne serais pas UI approved)
        /// -> Solution en attente : créer une page à part (formulaire) et gérer le Update là bas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[RelayCommand]
        //public async System.Threading.Tasks.Task EditTaskAsync(int id)
        //{
        //    //1. Find the task
        //    TaskManager.Models.Task task = Tasks.FirstOrDefault(t => t.Id == id);
        //    //2. Delete the old task
        //    await DeleteTaskAsync(id); //in db
        //    Tasks.RemoveAt(Tasks.IndexOf(task)); //in vars
        //    //3. Save the task
        //    await _context.AddItemAsync<Task>(task);
        //    Tasks.Add(task);
        //}

        [RelayCommand]
        public async System.Threading.Tasks.Task LoadTasksAsync()
        {
            var tasks = await _context.GetAllAsync<Task>();
            if (tasks is not null && tasks.Any())
            {
                Tasks ??= new ObservableCollection<Task>();
                foreach (TaskManager.Models.Task task in tasks)
                {
                    Tasks.Add(task);
                }
            }
        }

        [RelayCommand]
        private void SetOperatingTask(Task? task) => OperatingTask = task ?? new();

        [RelayCommand]
        private async System.Threading.Tasks.Task SaveTaskAsync()
        {
            if (OperatingTask is null)
            {
                return;
            }

            //validate the task before saving
            (bool isValid, string? errorMessage) = OperatingTask.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Erreur de validation", errorMessage, "Ok");
                return;
            }

            string busyText = OperatingTask.Id == 0 ? "Creating task..." : "Updating task...";
            await ExecuteAsync(async () =>
            {
                if (OperatingTask.Id == 0)
                {
                    // Create Task
                    await _context.AddItemAsync<Task>(OperatingTask);
                    Tasks.Add(OperatingTask);
                }
                //else
                //{
                //    await _context.UpdateItemAsync<Task>(OperatingTask);
                //    TaskManager.Models.Task TaskCopy = OperatingTask.Clone();
                //    int index = Tasks.IndexOf(OperatingTask);
                //    Tasks.RemoveAt(index);
                //    Tasks.Insert(index, TaskCopy);
                //}
                SetOperatingTaskCommand.Execute(new());
            }, busyText);
        }

        /// <summary>
        /// Deletes a specific task
        /// </summary>
        /// <param name="id">The task's id</param>
        /// <returns></returns>
        [RelayCommand]
        private async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByIdAsync<Task>(id))
                {
                    TaskManager.Models.Task task = Tasks.FirstOrDefault(t => t.Id == id);
                    Tasks.Remove(task);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Task was not deleted", "Ok");
                }
            }, "Deleting task");
       }

        private async System.Threading.Tasks.Task ExecuteAsync(Func<System.Threading.Tasks.Task> operation, string? busyText = null)
        {
            IsBusy = true;
            Busytext = busyText ?? "Processing...";
            try
            {
                await operation?.Invoke();
            }
            finally
            {
                IsBusy = false;
                Busytext = "Processing...";
            }
        }
    }
}
