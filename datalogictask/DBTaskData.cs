using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class DbTaskData : ITaskData
    {
        private static readonly string connectionString =
            "Data Source=DESKTOP-53PHH4Q;Initial Catalog=DbTaskData;Integrated Security=True;TrustServerCertificate=True;";

        public List<TaskItem> GetAllTasks(string user)
        {
            var tasks = new List<TaskItem>();
            string query = "SELECT Id, Task, DateAndTime FROM Tasks WHERE User = @User";

            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@User", user);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var task = new TaskItem(
                    user,
                    reader["Task"]?.ToString() ?? string.Empty,
                    Convert.ToDateTime(reader["DateAndTime"])
                );
                tasks.Add(task);
            }
            return tasks;
        }
        public void AddTask(string user, string taskDescription)
        {
            const string query = "INSERT INTO Tasks (User, Task, DateAndTime) VALUES (@User, @Task, @DateAndTime)";
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@User", user);
            cmd.Parameters.AddWithValue("@Task", taskDescription);
            cmd.Parameters.AddWithValue("@DateAndTime", DateTime.Now);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public bool EditTask(int index, string newDescription, string user)
        {
            int taskId = GetTaskIdByIndex(index, user);
            if (taskId == -1) return false;

            const string query = "UPDATE Tasks SET Task = @Task WHERE Id = @Id";
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Task", newDescription);
            cmd.Parameters.AddWithValue("@Id", taskId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteTask(int index, string user)
        {
            int taskId = GetTaskIdByIndex(index, user);
            if (taskId == -1) return false;

            const string query = "DELETE FROM Tasks WHERE Id = @Id";
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Id", taskId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool MarkAsDone(int index, string user)
        {
            int taskId = GetTaskIdByIndex(index, user);
            if (taskId == -1) return false;

  
            var task = GetTaskById(taskId, user);
            if (task == null) return false;
            if (!task.Task.StartsWith("[√] "))
            {
                string newTaskText = "[√] " + task.Task;
                const string query = "UPDATE Tasks SET Task = @Task WHERE Id = @Id";
                using SqlConnection conn = new(connectionString);
                using SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@Task", newTaskText);
                cmd.Parameters.AddWithValue("@Id", taskId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            return true;
        }

        public List<TaskItem> SearchTasks(string keyword, string user)
        {
            var result = new List<TaskItem>();
            const string query = "SELECT Id, Task, DateAndTime FROM Tasks WHERE User = @User AND Task LIKE @Keyword";

            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@User", user);
            cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var task = new TaskItem(
                    user,
                    reader["Task"]?.ToString() ?? string.Empty,
                    Convert.ToDateTime(reader["DateAndTime"])
                );
                result.Add(task);
            }
            return result;
        }

        private static int GetTaskIdByIndex(int index, string user)
        {
            const string query = "SELECT Id FROM Tasks WHERE User = @User ORDER BY Id";
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@User", user);
            conn.Open();
            using var reader = cmd.ExecuteReader();

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

        private static TaskItem GetTaskById(int id, string user)
        {
            const string query = "SELECT Task, DateAndTime FROM Tasks WHERE Id = @Id AND User = @User";
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@User", user);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new TaskItem(
                    user,
                    reader["Task"]?.ToString() ?? string.Empty,
                    Convert.ToDateTime(reader["DateAndTime"])
                );
            }
            return null;
        }
    }
}