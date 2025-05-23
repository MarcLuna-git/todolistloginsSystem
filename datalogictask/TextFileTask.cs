using System;
using System.Collections.Generic;
using System.IO;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class TextFileTask : ITaskData
    {
        private readonly string filePath = "tasks.txt";

        public List<TaskItem> GetAllTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        tasks.Add(new TaskItem(parts[0])
                        {
                            DateAndTime = DateTime.Parse(parts[1])
                        });
                    }
                }
            }
            return tasks;
        }

        public void AddTask(string taskDescription)
        {
            File.AppendAllText(filePath, taskDescription + "|" + DateTime.Now + Environment.NewLine);
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
                tasks[index].Task = "[\u221A] " + tasks[index].Task;
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public List<TaskItem> SearchTasks(string keyword)
        {
            List<TaskItem> result = new List<TaskItem>();
            foreach (var task in GetAllTasks())
            {
                if (task.Task.ToLower().Contains(keyword.ToLower()))
                {
                    result.Add(task);
                }
            }
            return result;
        }

        private void SaveAllTasks(List<TaskItem> tasks)
        {
            List<string> lines = new List<string>();
            foreach (var task in tasks)
            {
                lines.Add(task.Task + "|" + task.DateAndTime);
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}