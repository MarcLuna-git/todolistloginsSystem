using System;

namespace ToDoListProcess.Common
{
    public class TaskItem
    {
        public string Task { get; set; }
        public DateTime DateAndTime { get; set; }

        public TaskItem(string task)
        {
            Task = task;
            DateAndTime = DateTime.Now;
        }
    }
}
