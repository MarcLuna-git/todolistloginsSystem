using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class TextFileUserAccess : IUserAccess
    {
        private readonly string filePath = "users.txt";
        private List<Account> users = new();

        public TextFileUserAccess()
        {
            LoadUsersFromFile();
        }

        private void LoadUsersFromFile()
        {
            if (!File.Exists(filePath))
            {
                users = new List<Account>
                {
                    new Account("alice", "1234"),
                    new Account("bob", "5678"),
                    new Account("charlie", "abcd")
                };
                SaveUsersToFile();
                return;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    users.Add(new Account(parts[0], parts[1]));
                }
            }
        }

        private void SaveUsersToFile()
        {
            var lines = users.Select(u => $"{u.Username}|{u.Password}").ToArray();
            File.WriteAllLines(filePath, lines);
        }

        public bool ValidateCredentials(string username, string password)
        {
            return users.Any(u => u.Username == username && u.Password == password);
        }

        public void AddUser(Account user)
        {
            users.Add(user);
            SaveUsersToFile();
        }

        public List<Account> GetAllUsers()
        {
            return new List<Account>(users);
        }
    }
}