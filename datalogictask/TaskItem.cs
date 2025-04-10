using System;

namespace ToDoListProcess.DL
{
    public class TaskItem
    {
        public string Task { get; set; }
        public DateTime DateAndTime { get; set; }
        public TaskItem(string taskDescription)
        {
            Task = taskDescription;
            DateAndTime = DateTime.Now;
        }
    }
}
