using System;
using System.Collections.Generic;
using System.IO;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class TextFileTask : ITaskData
    {
        private readonly string filePath = "tasks.txt";

        public List<TaskItem> GetAllTasks(string user)
        {
            var tasks = new List<TaskItem>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        var taskUser = parts[0];
                        var taskDescription = parts[1];
                        if (DateTime.TryParse(parts[2], out DateTime dateTime))
                        {
                            if (taskUser.Equals(user, StringComparison.OrdinalIgnoreCase))
                            {
                                var taskItem = new TaskItem(taskUser, taskDescription, dateTime);
                                tasks.Add(taskItem);
                            }
                        }
                    }
                }
            }
            return tasks;
        }

        public void AddTask(string user, string taskDescription)
        {
            var line = $"{user}|{taskDescription}|{DateTime.Now}";
            File.AppendAllText(filePath, line + Environment.NewLine);
        }

        public bool EditTask(int index, string newDescription, string user)
        {
            var tasks = GetAllTasks(user);
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].Task = newDescription; 
                SaveAllTasks(tasks, user);
                return true;
            }
            return false;
        }
        public bool DeleteTask(int index, string user)
        {
            var tasks = GetAllTasks(user);
            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                SaveAllTasks(tasks, user);
                return true;
            }
            return false;
        }

        public bool MarkAsDone(int index, string user)
        {
            var tasks = GetAllTasks(user);
            if (index >= 0 && index < tasks.Count)
            {
                var task = tasks[index];
                if (!task.Task.StartsWith("√ "))
                {
                    task.Task = "√ " + task.Task;
                }
                SaveAllTasks(tasks, user);
                return true;
            }
            return false;
        }

        public List<TaskItem> SearchTasks(string keyword, string user)
        {
            var results = new List<TaskItem>();
            foreach (var task in GetAllTasks(user))
            {
                if (task.Task.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(task);
                }
            }
            return results;
        }

        private void SaveAllTasks(List<TaskItem> tasks, string user)
        {
            // Read all existing lines to preserve other users' tasks
            var existingLines = File.Exists(filePath) ? File.ReadAllLines(filePath) : Array.Empty<string>();
            var newLines = new List<string>();

            foreach (var line in existingLines)
            {
                var parts = line.Split('|');
                if (parts.Length == 3 && parts[0].Equals(user, StringComparison.OrdinalIgnoreCase))
                {
                    // Skip existing tasks for this user, as we'll rewrite them
                }
                else
                {
                    newLines.Add(line);
                }
            }

            // Add tasks for the current user
            foreach (var task in tasks)
            {
                newLines.Add($"{task.User}|{task.Task}|{task.DateAndTime}");
            }

            // Write back all lines
            File.WriteAllLines(filePath, newLines);
        }
    }
}