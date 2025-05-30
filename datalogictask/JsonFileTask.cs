﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class JsonFileTask : ITaskData
    {
        private readonly string filePath = "tasks.json";

        public List<TaskItem> GetAllTasks()
        {
            if (!File.Exists(filePath))
                return new List<TaskItem>();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new();
        }

        public void AddTask(string taskDescription)
        {
            var tasks = GetAllTasks();
            tasks.Add(new TaskItem(taskDescription));
            SaveAllTasks(tasks);
        }

        public bool EditTask(int index, string newDescription)
        {
            var tasks = GetAllTasks();
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].Task = newDescription;
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public bool DeleteTask(int index)
        {
            var tasks = GetAllTasks();
            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public bool MarkAsDone(int index)
        {
            var tasks = GetAllTasks();
            if (index >= 0 && index < tasks.Count)
            {
                if (!tasks[index].Task.StartsWith("[√]"))
                    tasks[index].Task = "[√] " + tasks[index].Task;
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public List<TaskItem> SearchTasks(string keyword)
        {
            var results = new List<TaskItem>();
            foreach (var task in GetAllTasks())
            {
                if (task.Task.ToLower().Contains(keyword.ToLower()))
                    results.Add(task);
            }
            return results;
        }

        private void SaveAllTasks(List<TaskItem> tasks)
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePath, json);
        }
    }
}
