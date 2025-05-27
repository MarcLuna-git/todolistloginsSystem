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

        public bool Authenticate(string username, string password)
        {
            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
