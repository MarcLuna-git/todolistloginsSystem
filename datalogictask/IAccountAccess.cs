using ToDoListProcess.Common;

namespace ToDoListProcess.DL
{
    public interface IAccountAccess
    {
        bool ValidateCredentials(string username, string password);
    }
}
