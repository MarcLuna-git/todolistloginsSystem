using System.Collections.Generic;
using ToDoListProcess.Common;

namespace ToDoListProcess.BL
{
    public class ToDoListManager
    {
        private readonly ITaskData taskData;

        public ToDoListManager(ITaskData dataSource)
        {
            taskData = dataSource;
        }

        public List<string> GetTasks()
        {
            var tasks = taskData.GetAllTasks();
            List<string> taskList = new List<string>();
            foreach (var task in tasks)
            {
                taskList.Add($"{task.Task} {task.DateAndTime:yyyy-MM-dd HH:mm:ss}");
            }
            return taskList;
        }

        public void AddTask(string taskDescription)
        {
            taskData.AddTask(taskDescription);
        }

        public bool EditTask(int index, string newDescription)
        {
            return taskData.EditTask(index, newDescription);
        }

        public bool DeleteTask(int index)
        {
            return taskData.DeleteTask(index);
        }

        public bool MarkAsDone(int index)
        {
            return taskData.MarkAsDone(index);
        }

        public List<string> SearchTasks(string keyword)
        {
            var tasks = taskData.SearchTasks(keyword);
            List<string> formattedResults = new List<string>();
            foreach (var task in tasks)
            {
                formattedResults.Add($"{task.Task} {task.DateAndTime:yyyy-MM-dd HH:mm:ss}");
            }
            return formattedResults;
        }
    }
}
