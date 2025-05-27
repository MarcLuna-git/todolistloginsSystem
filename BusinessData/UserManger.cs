using System.Collections.Generic;
using ToDoListProcess.Common;

namespace ToDoListProcess.BL
{
    public class UserManager
    {
        private readonly List<User> users = new()
        {
            new User("Marc", "1234"),
            new User("Laurence", "2004"),
            new User("Luna", "0924")
        };

        public LoginStatus Authenticate(string username, string password)
        {
            var user = users.Find(u => u.Username == username);
            if (user == null)
                return LoginStatus.UserNotFound;

            if (user.Password != password)
                return LoginStatus.WrongPassword;

            return LoginStatus.Success;
        }
    }
}
