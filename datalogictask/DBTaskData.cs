using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class DbTaskData : ITaskData
    {
        private static readonly string connectionString =
            "Data Source=DESKTOP-53PHH4Q;Initial Catalog=DBTaskData;Integrated Security=True;TrustServerCertificate=True;";

        public List<TaskItem> GetAllTasks()
        {
            var tasks = new List<TaskItem>();
            const string query = "SELECT Task, DateAndTime FROM Tasks";
             
            using SqlConnection conn = new(connectionString);
            using SqlCommand command = new(query, conn);

            conn.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string task = reader["Task"]?.ToString() ?? string.Empty;
                DateTime date = Convert.ToDateTime(reader["DateAndTime"]);
                tasks.Add(new TaskItem(task) { DateAndTime = date });
            }

            return tasks;
        }

        public void AddTask(string taskDescription)
        {
            const string query = "INSERT INTO Tasks (Task, DateAndTime) VALUES (@Task, @DateAndTime)";

            using SqlConnection conn = new(connectionString);
            using SqlCommand command = new(query, conn);
            command.Parameters.AddWithValue("@Task", taskDescription);
            command.Parameters.AddWithValue("@DateAndTime", DateTime.Now);

            conn.Open();
            command.ExecuteNonQuery();
        }

        public bool EditTask(int index, string newDescription)
        {
            int taskId = GetTaskIdByIndex(index);
            if (taskId == -1) return false;

            const string query = "UPDATE Tasks SET Task = @Task WHERE Id = @Id";
            using SqlConnection conn = new(connectionString);
            using SqlCommand command = new(query, conn);
            command.Parameters.AddWithValue("@Task", newDescription);
            command.Parameters.AddWithValue("@Id", taskId);

            conn.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool DeleteTask(int index)
        {
            int taskId = GetTaskIdByIndex(index);
            if (taskId == -1) return false;

            const string query = "DELETE FROM Tasks WHERE Id = @Id";
            using SqlConnection conn = new(connectionString);
            using SqlCommand command = new(query, conn);
            command.Parameters.AddWithValue("@Id", taskId);

            conn.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool MarkAsDone(int index)
        {
            var tasks = GetAllTasks();
            if (index < 0 || index >= tasks.Count) return false;

            string currentTask = tasks[index].Task;
            if (currentTask.StartsWith("[√]")) return true;

            return EditTask(index, "[√] " + currentTask);
        }

        public List<TaskItem> SearchTasks(string keyword)
        {
            var result = new List<TaskItem>();
            const string query = "SELECT Task, DateAndTime FROM Tasks WHERE Task LIKE @Keyword";

            using SqlConnection conn = new(connectionString);
            using SqlCommand command = new(query, conn);
            command.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

            conn.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string task = reader["Task"]?.ToString() ?? string.Empty;
                DateTime date = Convert.ToDateTime(reader["DateAndTime"]);
                result.Add(new TaskItem(task) { DateAndTime = date });
            }

            return result;
        }

        private int GetTaskIdByIndex(int index)
        {
            const string query = "SELECT Id FROM Tasks ORDER BY Id";
            using SqlConnection conn = new(connectionString);
            using SqlCommand command = new(query, conn);

            conn.Open();
            using SqlDataReader reader = command.ExecuteReader();

            int count = 0;
            while (reader.Read())
            {
                if (count == index)
                {
                    return Convert.ToInt32(reader["Id"]);
                }
                count++;
            }

            return -1;
        }
    }
}
