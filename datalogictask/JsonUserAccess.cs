using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class JsonUserAccess : IUserAccess
    {
        private readonly string filePath = "users.json";

        public JsonUserAccess()
        {
            if (!File.Exists(filePath))
            {
                var defaultUsers = new List<Account>
                {
                    new Account("Marc", "1234"),
                    new Account("Laurene", "2004"),
                    new Account("Luna", "0924")
                };
                SaveAllUsers(defaultUsers);
            }
        }

        public bool ValidateCredentials(string username, string password)
        {
            return GetAllUsers().Any(u => u.Username == username && u.Password == password);
        }

        public void AddUser(Account user)
        {
            var users = GetAllUsers();
            users.Add(user);
            SaveAllUsers(users);
        }

        public List<Account> GetAllUsers()
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();
        }

        private void SaveAllUsers(List<Account> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}