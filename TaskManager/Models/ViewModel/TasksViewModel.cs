using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Task> _tasks;

        [ObservableProperty]
        private Task _operatingTask = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busytext;


        private void SetOperatingTask(Task? task) => OperatingTask = task ?? new();

        [RelayCommand]
        private async System.Threading.Tasks.Task LoadTasksAsync()
        {
            var tasks = await _context.GetAllAsync<Task>();
            if (tasks is not null && tasks.Any())
            {
                Tasks ??= new ObservableCollection<Task>();
                foreach (var task in Tasks)
                {
                    Tasks.Add(task);
                }
            }
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task SaveTaskAsync()
        {
            if (OperatingTask is null)
            {
                return;
            }

            if (OperatingTask.Id == 0)
            {
                // Create Task
                await _context.AddItemAsync<Task>(OperatingTask);
                Tasks.Add(OperatingTask);
            }
            else
            {
                // Update Task
                await _context.UpdateItemAsync<Task>(OperatingTask);
                Task TaskCopy = OperatingTask.Clone();
                var index = Tasks.IndexOf(OperatingTask);
                Tasks.RemoveAt(index);
                Tasks.Insert(index, TaskCopy);
            }
            //SetOperatingTaskCommand.Execute(new()); //not working :(
        }

        /// <summary>
        /// Deletes a specific task
        /// </summary>
        /// <param name="id">The task's id</param>
        /// <returns>Nothing</returns>
        [RelayCommand]
        private async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            IsBusy = true;
            Busytext = "Deleting task";
            try
            {
                if (await _context.DeleteItemByIdAsync<Task>(id))
                {
                    Task task = Tasks.FirstOrDefault(t => t.Id == id);
                    Tasks.Remove(task);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Task was not deleted", "Ok");
                }
            }
            finally
            {
                IsBusy = false;
                Busytext = "Processing...";
            }
        }
    }
}
