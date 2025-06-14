using System.Collections.Generic;
using ToDoListProcess.Common;
using ToDoListProcess.DL;

namespace ToDoListProcess.Business
{
    public class ToDoListManager
    {
        private ITaskData taskData;

        public ToDoListManager(ITaskData taskData)
        {
            this.taskData = taskData;
        }

        public List<string> GetTasks(string username)
        {
            List<TaskItem> tasks = taskData.GetAllTasks(username);
            List<string> result = new List<string>();

            foreach (TaskItem task in tasks)
            {
                result.Add(task.Task + " " + task.DateAndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            return result;
        }

        public void AddTask(string username, string description)
        {
            taskData.AddTask(username, description);
        }

        public bool EditTask(int index, string newDescription, string username)
        {
            return taskData.EditTask(index, newDescription, username);
        }

        public bool DeleteTask(int index, string username)
        {
            return taskData.DeleteTask(index, username);
        }

        public bool MarkAsDone(int index, string username)
        {
            return taskData.MarkAsDone(index, username);
        }

        public List<string> SearchTasks(string keyword, string username)
        {
            List<TaskItem> tasks = taskData.SearchTasks(keyword, username);
            List<string> result = new List<string>();

            foreach (TaskItem task in tasks)
            {
                result.Add(task.Task + " " + task.DateAndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            return result;
        }
    }
}