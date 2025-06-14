using System.Collections.Generic;
using ToDoListProcess.Common;

public class UserManager
{
    private List<User> users;

    public UserManager(List<User> users)
    {
        this.users = users;
    }

    public bool Authenticate(string username, string password)
    {
        foreach (User user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                return true;
            }
        }
        return false;
    }

    public bool Register(string username, string password)
    {
        foreach (User user in users)
        {
            if (user.Username == username)
            {
                return false;
            }
        }

        users.Add(new User(username, password));
        return true;
    }
}