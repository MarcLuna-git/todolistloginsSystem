﻿using System;
using System.Collections.Generic;
using ToDoListProcess.DL;  // Data Logic

namespace ToDoListProcess.BL
{
    public class ToDoListManager
    {
        private TaskManager taskManager;

        public static string PinCode { get; } = "2004";  // Static PinCode for validation

        public ToDoListManager()
        {
            taskManager = new TaskManager();  // Calling the DL 
        }

        public List<string> GetTasks()
        {
            var tasks = taskManager.GetAllTasks();
            List<string> taskList = new List<string>();

            foreach (var task in tasks)
            {
                taskList.Add($"{task.Task} {task.DateAndTime:yyyy-MM-dd HH:mm:ss}");
            }

            return taskList;
        }

        public void AddTask(string taskDescription)
        {
            taskManager.AddTask(taskDescription);
        }

        public bool EditTask(int index, string newDescription)
        {
            return taskManager.EditTask(index, newDescription);
        }

        public bool DeleteTask(int index)
        {
            return taskManager.DeleteTask(index);
        }

        public bool MarkAsDone(int index)
        {
            return taskManager.MarkAsDone(index);
        }

        public List<string> SearchTasks(string keyword)
        {
            var tasks = taskManager.SearchTasks(keyword);
            List<string> formattedResults = new List<string>();

            foreach (var task in tasks)
            {
                formattedResults.Add($"{task.Task} {task.DateAndTime:yyyy-MM-dd HH:mm:ss}");
            }

            return formattedResults;
        }
    }
}
