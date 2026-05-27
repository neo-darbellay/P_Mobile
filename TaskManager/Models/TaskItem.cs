using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TaskItem
    {
        /// <summary>
        /// The id is unique and not-null, it referes to a specific instance of the object
        /// </summary>
        public int Id {  get; set; }

        /// <summary>
        /// The task's name : like "Feed the cat", must be not-null
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A nullable description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// A boolean that defines the state of the task : true if done, false if to do
        /// </summary>
        public bool Done { get; set; } = false;
    }
}
