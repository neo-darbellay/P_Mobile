using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	/// <summary>
	/// A task is a goal the user can create and try to complete. 
	/// </summary>
    public class Task
    {
		/// <summary>
		/// The id is unique and not null, it define the instance of the object
		/// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The name of the task
        /// </summary>
        private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// A facultative short paragraph that defines what needs to be done for the task to close
		/// </summary>
		private string _description;
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// Defines if the task is closed
		/// </summary>
		private bool _done;
		public bool Done
		{
			get { return _done; }
			set { _done = value; }
		}

		public TaskManager.Models.Task Clone() => MemberwiseClone() as TaskManager.Models.Task;

		/// <summary>
		/// Validates the fields when creating/updating a task
		/// </summary>
		/// <returns></returns>
		public (bool Isvalid, string? ErrorMessage) Validate()
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				return (false, $"{nameof(Name)} is required");
			}
			else if (string.IsNullOrWhiteSpace(Description))
            {
                return (false, $"{nameof(Description)} is required");
            }
			return (true, null);
        }

	}
}
