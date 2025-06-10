using System.Collections.Generic;
using ToDoListProcess.Common;

namespace ToDoListProcess.Business
{
    public class ToDoListManager(ITaskData taskData)
    {
        private readonly ITaskData _taskData = taskData;

        public List<string> GetTasks(string username)
        {
            var tasks = _taskData.GetAllTasks(username); 
            var list = new List<string>();
            foreach (var task in tasks)
            {
                list.Add($"{task.Task} {task.DateAndTime:yyyy-MM-dd HH:mm:ss}");
            }
            return list;
        }

        public void AddTask(string user, string taskDescription)
        {
            _taskData.AddTask(user, taskDescription);
        }

        public bool EditTask(int index, string newDescription, string username)
        {
            return _taskData.EditTask(index, newDescription, username);
        }

        public bool DeleteTask(int index, string username)
        {
            return _taskData.DeleteTask(index, username);
        }

        public bool MarkAsDone(int index, string username)
        {
            return _taskData.MarkAsDone(index, username);
        }

        public List<string> SearchTasks(string keyword, string username)
        {
            var tasks = _taskData.SearchTasks(keyword, username);
            var list = new List<string>();
            foreach (var task in tasks)
            {
                list.Add($"{task.Task} {task.DateAndTime:yyyy-MM-dd HH:mm:ss}");
            }
            return list;
        }
    }
}