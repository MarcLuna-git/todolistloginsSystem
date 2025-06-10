using System;
using System.Collections.Generic;
using System.Linq;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class InMemoryTask : ITaskData
    {
        private readonly List<TaskItem> tasks = new();

        public List<TaskItem> GetAllTasks(string user)
        {
            var userTasks = new List<TaskItem>();
            foreach (var task in tasks)
            {
                if (task.User == user)
                {
                    userTasks.Add(task);
                }
            }
            return userTasks;
        }

        public void AddTask(string user, string taskDescription)
        {
            tasks.Add(new TaskItem(user, taskDescription, DateTime.Now));
        }

        public bool EditTask(int index, string newDescription, string user)
        {
            var userTasks = GetAllTasks(user);
            if (index >= 0 && index < userTasks.Count)
            {
                var taskToEdit = userTasks[index];
                
                int originalIndex = tasks.IndexOf(taskToEdit);
                if (originalIndex != -1)
                {
                    var updatedTask = new TaskItem(user, newDescription, tasks[originalIndex].DateAndTime);
                    tasks[originalIndex] = updatedTask;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteTask(int index, string user)
        {
            var userTasks = GetAllTasks(user);
            if (index >= 0 && index < userTasks.Count)
            {
                var taskToRemove = userTasks[index];
                tasks.Remove(taskToRemove);
                return true;
            }
            return false;
        }

        public bool MarkAsDone(int index, string user)
        {
            var userTasks = GetAllTasks(user);
            if (index >= 0 && index < userTasks.Count)
            {
                var task = userTasks[index];
                int originalIndex = tasks.IndexOf(task);
                if (originalIndex != -1 && !tasks[originalIndex].Task.StartsWith("[√] "))
                {
                    var completedTask = new TaskItem(user, "[√] " + task.Task, task.DateAndTime);
                    tasks[originalIndex] = completedTask;
                }
                return true;
            }
            return false;
        }

        public List<TaskItem> SearchTasks(string keyword, string user)
        {
            var results = new List<TaskItem>();
            foreach (var task in tasks)
            {
                if (task.User == user && task.Task.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(task);
                }
            }
            return results;
        }
    }
}