    using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class DbUserManager
    {
        private static string connectionString =
            "Data Source=DESKTOP-53PHH4Q;Initial Catalog=DbTaskData;Integrated Security=True;TrustServerCertificate=True;";

        private readonly SqlConnection sqlConnection;

        public DbUserManager()
        {
            sqlConnection = new SqlConnection(connectionString);
        }
        public List<string> GetAllUsers()
        {
            var users = new List<string>();
            string selectStatement = "SELECT Username FROM Users"; 

            SqlCommand command = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(reader["Username"].ToString());
            }

            sqlConnection.Close();
            return users;
        }

    }
}
