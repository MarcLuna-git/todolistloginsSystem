using System.Collections.Generic;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class InMemoryTask : ITaskData
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public List<TaskItem> GetAllTasks()
        {
            return tasks;
        }

        public void AddTask(string taskDescription)
        {
            tasks.Add(new TaskItem(taskDescription));
        }

        public bool EditTask(int index, string newDescription)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].Task = newDescription;
                return true;
            }
            return false;
        }

        public bool DeleteTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                return true;
            }
            return false;
        }

        public bool MarkAsDone(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].Task = "[√] " + tasks[index].Task;
                return true;
            }
            return false;
        }

        public List<TaskItem> SearchTasks(string keyword)
        {
            List<TaskItem> result = new List<TaskItem>();
            foreach (var task in tasks)
            {
                if (task.Task.ToLower().Contains(keyword.ToLower()))
                {
                    result.Add(task);
                }
            }
            return result;
        }
    }
}