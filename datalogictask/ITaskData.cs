using System.Collections.Generic;

namespace ToDoListProcess.DL
{
    public interface ITaskData
    {
        List<TaskItem> GetAllTasks(string user);
        void AddTask(string user, string taskDescription);
        bool EditTask(int index, string newDescription, string user);
        bool DeleteTask(int index, string user);
        bool MarkAsDone(int index, string user);
        List<TaskItem> SearchTasks(string keyword, string user);
    }
}