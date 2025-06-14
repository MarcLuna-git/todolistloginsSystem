using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class JsonFileTask : ITaskData
    {
        private string filePath = "tasks.json";

        private List<TaskItem> LoadAllTasks()
        {
            if (!File.Exists(filePath))
            {
                return new List<TaskItem>();
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json);
        }

        private void SaveAllTasks(List<TaskItem> tasks)
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePath, json);
        }

        public List<TaskItem> GetAllTasks(string user)
        {
            List<TaskItem> allTasks = LoadAllTasks();
            List<TaskItem> userTasks = new List<TaskItem>();

            foreach (TaskItem task in allTasks)
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
            List<TaskItem> tasks = LoadAllTasks();
            tasks.Add(new TaskItem(user, taskDescription, DateTime.Now));
            SaveAllTasks(tasks);
        }

        public bool EditTask(int index, string newDescription, string user)
        {
            List<TaskItem> tasks = LoadAllTasks();
            List<TaskItem> userTasks = GetAllTasks(user);

            if (index >= 0 && index < userTasks.Count)
            {
                TaskItem target = userTasks[index];
                foreach (TaskItem task in tasks)
                {
                    if (task.User == user && task.Task == target.Task && task.DateAndTime == target.DateAndTime)
                    {
                        task.Task = newDescription;
                        SaveAllTasks(tasks);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool DeleteTask(int index, string user)
        {
            List<TaskItem> tasks = LoadAllTasks();
            List<TaskItem> userTasks = GetAllTasks(user);

            if (index >= 0 && index < userTasks.Count)
            {
                tasks.Remove(userTasks[index]);
                SaveAllTasks(tasks);
                return true;
            }

            return false;
        }

        public bool MarkAsDone(int index, string user)
        {
            List<TaskItem> tasks = LoadAllTasks();
            List<TaskItem> userTasks = GetAllTasks(user);

            if (index >= 0 && index < userTasks.Count)
            {
                TaskItem target = userTasks[index];

                foreach (TaskItem task in tasks)
                {
                    if (task.User == user && task.Task == target.Task && task.DateAndTime == target.DateAndTime)
                    {
                        if (!task.Task.StartsWith("[√] "))
                        {
                            task.Task = "[√] " + task.Task;
                        }
                        SaveAllTasks(tasks);
                        return true;
                    }
                }
            }

            return false;
        }

        public List<TaskItem> SearchTasks(string keyword, string user)
        {
            List<TaskItem> result = new List<TaskItem>();

            foreach (TaskItem task in GetAllTasks(user))
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
