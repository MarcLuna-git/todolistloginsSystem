using System.Collections.Generic;
using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public interface IUserAccess
    {
        bool ValidateCredentials(string username, string password);
        void AddUser(Account user);
        List<Account> GetAllUsers();
    }
}