using System.Collections.Generic;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public class InMemoryAccountAccess : IAccountAccess
    {
        private List<Account> accounts = new()
        {
            new Account("Marc", "1234"),
            new Account("Laurence", "2004"),
            new Account("Luna", "0924")
        };

        public bool ValidateCredentials(string username, string password)
        {
            return accounts.Exists(a => a.Username == username && a.Password == password);
        }
    }
}
