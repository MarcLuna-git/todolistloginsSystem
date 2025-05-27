using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class DbTaskData : ITaskData
    {
        private static string connectionString =
            "Data Source=DESKTOP-53PHH4Q;Initial Catalog=DBTaskData;Integrated Security=True;TrustServerCertificate=True;";

        private readonly SqlConnection sqlConnection;

        public DbTaskData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<TaskItem> GetAllTasks()
        {
            var tasks = new List<TaskItem>();
            string selectStatement = "SELECT Task, DateAndTime FROM Tasks";

            SqlCommand command = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var task = new TaskItem(reader["Task"].ToString())
                {
                    DateAndTime = Convert.ToDateTime(reader["DateAndTime"])
                };
                tasks.Add(task);
            }

            sqlConnection.Close();
            return tasks;
        }

        public void AddTask(string taskDescription)
        {
            string insertStatement = "INSERT INTO Tasks (Task, DateAndTime) VALUES (@Task, @DateAndTime)";

            SqlCommand command = new SqlCommand(insertStatement, sqlConnection);
            command.Parameters.AddWithValue("@Task", taskDescription);
            command.Parameters.AddWithValue("@DateAndTime", DateTime.Now);

            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public bool EditTask(int index, string newDescription)
        {
            var tasks = GetAllTasks();
            if (index < 0 || index >= tasks.Count) return false;

            string updateStatement = "UPDATE Tasks SET Task = @Task WHERE Id = @Id";
            int taskId = GetTaskIdByIndex(index);

            if (taskId == -1) return false;

            SqlCommand command = new SqlCommand(updateStatement, sqlConnection);
            command.Parameters.AddWithValue("@Task", newDescription);
            command.Parameters.AddWithValue("@Id", taskId);

            sqlConnection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0;
        }

        public bool DeleteTask(int index)
        {
            int taskId = GetTaskIdByIndex(index);
            if (taskId == -1) return false;

            string deleteStatement = "DELETE FROM Tasks WHERE Id = @Id";
            SqlCommand command = new SqlCommand(deleteStatement, sqlConnection);
            command.Parameters.AddWithValue("@Id", taskId);

            sqlConnection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0;
        }

        public bool MarkAsDone(int index)
        {
            var tasks = GetAllTasks();
            if (index < 0 || index >= tasks.Count) return false;

            string currentTask = tasks[index].Task;
            if (currentTask.StartsWith("[√]")) return true;

            string markedTask = "[√] " + currentTask;
            return EditTask(index, markedTask);
        }

        public List<TaskItem> SearchTasks(string keyword)
        {
            var result = new List<TaskItem>();
            string searchStatement = "SELECT Task, DateAndTime FROM Tasks WHERE Task LIKE @Keyword";

            SqlCommand command = new SqlCommand(searchStatement, sqlConnection);
            command.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var task = new TaskItem(reader["Task"].ToString())
                {
                    DateAndTime = Convert.ToDateTime(reader["DateAndTime"])
                };
                result.Add(task);
            }

            sqlConnection.Close();
            return result;
        }

        private int GetTaskIdByIndex(int index)
        {
            string selectStatement = "SELECT Id FROM Tasks ORDER BY Id";
            SqlCommand command = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();

            int count = 0;
            int taskId = -1;
            while (reader.Read())
            {
                if (count == index)
                {
                    taskId = Convert.ToInt32(reader["Id"]);
                    break;
                }
                count++;
            }

            sqlConnection.Close();
            return taskId;
        }
    }
}
