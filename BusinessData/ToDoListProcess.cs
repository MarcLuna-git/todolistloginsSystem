using System;
using System.Collections.Generic;

namespace ToDoListProcess
{
    public class ToDoListManager
    {
        private List<(string Task, DateTime DateAndTime)> tasks = new List<(string, DateTime)>(); //storing task with Date and Time
        public static string PinCode { get; } = "2004";

        public List<string> GetTasks()
        {
            List<string> taskList = new List<string>();
            foreach (var task in tasks)
            {
                taskList.Add($"{task.Task} {task.DateAndTime:yyyy-MM-dd HH:mm:ss}");
            }
            return taskList;
        }

        public void AddTask(string task) // Method for adding a task
        {
            tasks.Add((task, DateTime.Now));
        }

        public bool EditTask(int index, string newDescription)
        {
            if (IsValidIndex(index))
            {
                var DateTime = tasks[index - 1].DateAndTime; 
                tasks[index - 1] = (newDescription, DateTime);
                return true;
            }
            return false;
        }

        public bool DeleteTask(int index)
        {
            if (IsValidIndex(index))
            {
                tasks.RemoveAt(index - 1);
                return true;
            }
            return false;
        }

        public bool MarkAsDone(int index)
        {
            if (IsValidIndex(index))
            {
                var (task, timestamp) = tasks[index - 1];
                tasks[index - 1] = ($"[√] {task}", timestamp);
                return true;
            }
            return false;
        }

        private bool IsValidIndex(int index)
        {
            return index > 0 && index <= tasks.Count;
        }
    }
}
