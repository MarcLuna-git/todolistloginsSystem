using System;
using System.Collections.Generic;
using System.IO;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class TextFileTask : ITaskData
    {
        private string filePath = "tasks.txt";

        public List<TaskItem> GetAllTasks(string user)
        {
            List<TaskItem> tasks = new List<TaskItem>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    if (parts.Length == 3)
                    {
                        string taskUser = parts[0];
                        string taskText = parts[1];
                        DateTime date;

                        if (DateTime.TryParse(parts[2], out date))
                        {
                            if (taskUser == user)
                            {
                                tasks.Add(new TaskItem(taskUser, taskText, date));
                            }
                        }
                    }
                }
            }

            return tasks;
        }

        public void AddTask(string user, string taskDescription)
        {
            string line = user + "|" + taskDescription + "|" + DateTime.Now.ToString();
            File.AppendAllText(filePath, line + "\n");
        }

        public bool EditTask(int index, string newDescription, string user)
        {
            List<TaskItem> tasks = GetAllTasks(user);

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
            List<TaskItem> tasks = GetAllTasks(user);

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
            List<TaskItem> tasks = GetAllTasks(user);

            if (index >= 0 && index < tasks.Count)
            {
                if (!tasks[index].Task.StartsWith("[√] "))
                {
                    tasks[index].Task = "[√] " + tasks[index].Task;
                    SaveAllTasks(tasks, user);
                }
                return true;
            }

            return false;
        }

        public List<TaskItem> SearchTasks(string keyword, string user)
        {
            List<TaskItem> found = new List<TaskItem>();

            foreach (TaskItem task in GetAllTasks(user))
            {
                if (task.Task.ToLower().Contains(keyword.ToLower()))
                {
                    found.Add(task);
                }
            }

            return found;
        }

        private void SaveAllTasks(List<TaskItem> tasks, string user)
        {
            List<string> lines = new List<string>();

            if (File.Exists(filePath))
            {
                string[] existingLines = File.ReadAllLines(filePath);
                foreach (string line in existingLines)
                {
                    if (!line.StartsWith(user + "|"))
                    {
                        lines.Add(line);
                    }
                }
            }

            foreach (TaskItem task in tasks)
            {
                lines.Add(task.User + "|" + task.Task + "|" + task.DateAndTime);
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}