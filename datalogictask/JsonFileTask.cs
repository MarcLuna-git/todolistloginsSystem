using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToDoListProcess.Common;

namespace ToDoListProcess.Data
{
    public class JsonFileTask : ITaskData
    {
        private readonly string filePath = "tasks.json";

        private List<TaskItem> LoadAllTasks()
        {
            if (!File.Exists(filePath))
                return new List<TaskItem>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        private void SaveAllTasks(List<TaskItem> tasks)
        {
            var json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePath, json);
        }

        public List<TaskItem> GetAllTasks(string user)
        {
            var allTasks = LoadAllTasks();
            return allTasks.FindAll(t => t.User == user);
        }

        public void AddTask(string user, string taskDescription)
        {
            var tasks = LoadAllTasks();
            // Assuming constructor is TaskItem(string user, string task, DateTime dateAndTime)
            tasks.Add(new TaskItem(user, taskDescription, DateTime.Now));
            SaveAllTasks(tasks);
        }

        public bool EditTask(int index, string newDescription, string user)
        {
            var tasks = LoadAllTasks();
            var userTasks = tasks.FindAll(t => t.User == user);
            if (index >= 0 && index < userTasks.Count)
            {
                var task = userTasks[index];
                task.Task = newDescription;
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public bool DeleteTask(int index, string user)
        {
            var tasks = LoadAllTasks();
            var userTasks = tasks.FindAll(t => t.User == user);
            if (index >= 0 && index < userTasks.Count)
            {
                var taskToRemove = userTasks[index];
                tasks.Remove(taskToRemove);
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public bool MarkAsDone(int index, string user)
        {
            var tasks = LoadAllTasks();
            var userTasks = tasks.FindAll(t => t.User == user);
            if (index >= 0 && index < userTasks.Count)
            {
                var task = userTasks[index];
                if (!task.Task.StartsWith("[√] "))
                {
                    task.Task = "[√] " + task.Task;
                }
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public List<TaskItem> SearchTasks(string keyword, string user)
        {
            var allTasks = LoadAllTasks();
            return allTasks.FindAll(t => t.User == user && t.Task.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}