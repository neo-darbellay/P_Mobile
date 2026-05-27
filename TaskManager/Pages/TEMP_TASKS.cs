using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Pages
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public bool IsDone { get; set; }
        public List<Tag> Tags { get; set; } = new();
    }

    public class Tag
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
