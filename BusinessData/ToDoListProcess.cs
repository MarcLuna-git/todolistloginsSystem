using System;
using System.Collections.Generic;

namespace ToDoListProcess
{
    public class ToDoListManager
    {
        private List<string> tasks = new List<string>(); // Stores all tasks

        public List<string> GetTasks()
        {
            return new List<string>(tasks); 
        }

        public void AddTask(string task) //method for adding a task
        {
            tasks.Add(task);
        }

        public bool EditTask(int index, string newDescription)
        {
            if (IsValidIndex(index))
            {
                tasks[index - 1] = newDescription;
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
                tasks[index - 1] = "√ " + tasks[index - 1];
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
