using System;
using System.Collections.Generic;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class InMemoryTask : ITaskData
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public List<User> Users = new List<User>
        {
            new User("Marc", "1234"),
            new User("Laurence", "2004"),
            new User("Luna", "0924")
        };

        public List<TaskItem> GetAllTasks(string username)
        {
            List<TaskItem> result = new List<TaskItem>();

            foreach (TaskItem task in tasks)
            {
                if (task.User == username)
                {
                    result.Add(task);
                }
            }

            return result;
        }

        public void AddTask(string username, string description)
        {
            tasks.Add(new TaskItem(username, description, DateTime.Now));
        }

        public bool EditTask(int index, string newDescription, string username)
        {
            List<TaskItem> userTasks = GetAllTasks(username);

            if (index >= 0 && index < userTasks.Count)
            {
                TaskItem oldTask = userTasks[index];
                int realIndex = tasks.IndexOf(oldTask);

                if (realIndex >= 0)
                {
                    TaskItem newTask = new TaskItem(username, newDescription, oldTask.DateAndTime);
                    tasks[realIndex] = newTask;
                    return true;
                }
            }

            return false;
        }

        public bool DeleteTask(int index, string username)
        {
            List<TaskItem> userTasks = GetAllTasks(username);

            if (index >= 0 && index < userTasks.Count)
            {
                tasks.Remove(userTasks[index]);
                return true;
            }

            return false;
        }

        public bool MarkAsDone(int index, string username)
        {
            List<TaskItem> userTasks = GetAllTasks(username);

            if (index >= 0 && index < userTasks.Count)
            {
                TaskItem oldTask = userTasks[index];
                int realIndex = tasks.IndexOf(oldTask);

                if (realIndex >= 0 && !tasks[realIndex].Task.StartsWith("[√] "))
                {
                    TaskItem newTask = new TaskItem(username, "[√] " + oldTask.Task, oldTask.DateAndTime);
                    tasks[realIndex] = newTask;
                    return true;
                }
            }

            return false;
        }

        public List<TaskItem> SearchTasks(string keyword, string username)
        {
            List<TaskItem> result = new List<TaskItem>();

            foreach (TaskItem task in tasks)
            {
                if (task.User == username && task.Task.ToLower().Contains(keyword.ToLower()))
                {
                    result.Add(task);
                }
            }

            return result;
        }
    }
}