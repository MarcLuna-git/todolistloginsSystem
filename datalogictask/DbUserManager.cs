using System;
using System.Data;
using System.Data.SqlClient;
using todolist_Common;

namespace DataLogicLayer
{
    public class DbUserManager
    {
        private readonly string _connectionString;

        public DbUserManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserModel Authenticate(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString()
                            };
                        }
                    }
                }
            }

            return null; // mag return null sya if yung user is hindi makita or wrong yung password nya
        }
    }
}
