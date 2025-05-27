using System.Collections.Generic;

namespace ToDoListProcess.Common
{
    public interface ITaskData
    {
        List<TaskItem> GetAllTasks();
        void AddTask(string taskDescription);
        bool EditTask(int index, string newDescription);
        bool DeleteTask(int index);
        bool MarkAsDone(int index);
        List<TaskItem> SearchTasks(string keyword);
    }
}
