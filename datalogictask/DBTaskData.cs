using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class DbTaskData : ITaskData
    {
        string connectionString = "Data Source=DESKTOP-53PHH4Q;Initial Catalog=DbTaskData;Integrated Security=True;TrustServerCertificate=True;";


        public List<TaskItem> GetAllTasks(string user)
        {
            List<TaskItem> tasks = new List<TaskItem>();
            string query = "SELECT Task, DateAndTime FROM Task WHERE [User] = @User";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@User", user);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string task = reader["Task"].ToString();
                    DateTime date = Convert.ToDateTime(reader["DateAndTime"]);
                    tasks.Add(new TaskItem(user, task, date));
                }
            }

            return tasks;
        }

        public void AddTask(string user, string taskDescription)
        {
            string query = "INSERT INTO Task ([User], Task, DateAndTime) VALUES (@User, @Task, @DateAndTime)";


            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@User", user);
                cmd.Parameters.AddWithValue("@Task", taskDescription);
                cmd.Parameters.AddWithValue("@DateAndTime", DateTime.Now);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool EditTask(int index, string newDescription, string user)
        {
            List<TaskItem> tasks = GetAllTasks(user);

            if (index >= 0 && index < tasks.Count)
            {
                TaskItem task = tasks[index];

                string query = "UPDATE Task SET Task = @Task WHERE [User] = @User AND DateAndTime = @DateAndTime";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Task", newDescription);
                    cmd.Parameters.AddWithValue("@User", user);
                    cmd.Parameters.AddWithValue("@DateAndTime", task.DateAndTime);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }

            return false;
        }

        public bool DeleteTask(int index, string user)
        {
            List<TaskItem> tasks = GetAllTasks(user);

            if (index >= 0 && index < tasks.Count)
            {
                TaskItem task = tasks[index];

                string query = "DELETE FROM Task WHERE [User] = @User AND DateAndTime = @DateAndTime";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@User", user);
                    cmd.Parameters.AddWithValue("@DateAndTime", task.DateAndTime);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }

            return false;
        }

        public bool MarkAsDone(int index, string user)
        {
            List<TaskItem> tasks = GetAllTasks(user);

            if (index >= 0 && index < tasks.Count)
            {
                TaskItem task = tasks[index];

                if (!task.Task.StartsWith("[√] "))
                {
                    string newTask = "[√] " + task.Task;

                    string query = "UPDATE Task SET Task = @Task WHERE [User] = @User AND DateAndTime = @DateAndTime";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Task", newTask);
                        cmd.Parameters.AddWithValue("@User", user);
                        cmd.Parameters.AddWithValue("@DateAndTime", task.DateAndTime);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }

            return false;
        }

        public List<TaskItem> SearchTasks(string keyword, string user)
        {
            List<TaskItem> result = new List<TaskItem>();
            string query = "SELECT Task, DateAndTime FROM Task WHERE [User] = @User AND Task LIKE @Keyword";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@User", user);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string task = reader["Task"].ToString();
                    DateTime date = Convert.ToDateTime(reader["DateAndTime"]);
                    result.Add(new TaskItem(user, task, date));
                }
            }

            return result;
        }
    }
}