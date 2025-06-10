public class TaskItem
{
    public string User { get; }
    public string Task { get; set; } 
    public DateTime DateAndTime { get; }

    public TaskItem(string user, string task, DateTime dateAndTime)
    {
        User = user;
        Task = task;
        DateAndTime = dateAndTime;
    }
}