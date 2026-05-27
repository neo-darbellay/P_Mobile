§using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Services
{
    /// <summary>
    /// Une classe qui permet d'accèder aux données de la base de données (ici, des fichiers JSON)
    /// </summary>
    public class TaskItemService
    {
        /// <summary>
        /// The filepath of the JSON file
        /// </summary>
        private readonly string _filePath;

        public TaskItemService()
        {
            // Path to store the JSON file in app data
            _filePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "tasks.json"
            );
        }
        /// <summary>
        /// GET operation, retrieve all tasks
        /// </summary>
        /// <returns>A list of TaskItem</returns>
        public async Task<List<TaskItem>> LoadTasksAsync()
        {
            try
            {
                //empty list if the file doesn't exists
                if (!File.Exists(_filePath))
                {
                    return [];
                }
                //else we get the data
                string json = await File.ReadAllTextAsync(_filePath);
                List<TaskItem>? tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
                return tasks ?? []; //data or empty list if no data
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// PUT operation, rewrite the whole file to update some data
        /// </summary>
        /// <param name="tasks">The last version of the entire list of tasks</param>
        /// <returns>A task system</returns>
        public async Task SaveTasksAsync(List<TaskItem> tasks)
        {
            try
            {
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(tasks));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving: {ex.Message}");
            }
        }

        /// <summary>
        /// PATCH operation, rewrite the whole file but only take a single task to update
        /// </summary>
        /// <param name="task">The new task, with an id refering at the older version of the same task</param>
        /// <returns>A task system</returns>
        public async Task SaveTaskAsync(TaskItem task)
        {
            //GET all tasks
            List<TaskItem> tasks = await LoadTasksAsync();

            //separate the edited task and the rest
            TaskItem oldTask = tasks.Find(t => t.Id == task.Id);
            List<TaskItem> othersTasks = tasks.FindAll(t => t.Id != task.Id);

            //UPDATE the task and then the list
            oldTask = task;
            othersTasks.Add(task);

            //save the data
            try
            {
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(othersTasks));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving: {ex.Message}");
            }
        }

        /// <summary>
        /// CREATE operation, rewrite the whole file but only create a single task
        /// </summary>
        /// <param name="task">The new task</param>
        /// <returns>A task system</returns>
        public async Task CreateTaskAsync(TaskItem task)
        {
            //GET all tasks
            List<TaskItem> tasks = await LoadTasksAsync();

            //UPDATE the list
            tasks.Add(task);

            //save the data
            try
            {
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(tasks));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving: {ex.Message}");
            }
        }
    }
}
