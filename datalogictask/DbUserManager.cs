using Microsoft.Data.SqlClient;

public class DbUserManager
{
    string connectionString = "Data Source=DESKTOP-53PHH4Q;Initial Catalog=DbTaskData;Integrated Security=True;TrustServerCertificate=True;";

    public bool Authenticate(string username, string password)
    {
        string query = "SELECT COUNT(*) FROM Users WHERE Username=@Username AND Password=@Password";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }

    public bool Register(string username, string password)
    {
        string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
        {
            checkCmd.Parameters.AddWithValue("@Username", username);
            conn.Open();
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists > 0)
                return false;

            string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
            {
                insertCmd.Parameters.AddWithValue("@Username", username);
                insertCmd.Parameters.AddWithValue("@Password", password);
                insertCmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}

// Magrereference lang sya pag kinall nya yung database kaya sya kailangan