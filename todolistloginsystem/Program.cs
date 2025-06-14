using System;
using ToDoListProcess.Business;
using ToDoListProcess.Common;
using ToDoListProcess.DL;

namespace ToDoListUI
{
    internal class Program
    {
        static string? currentUser = null;

        //static readonly ITaskData taskData = new InMemoryTask();
        // static readonly ITaskData taskData = new JsonFileTask();
        // static readonly ITaskData taskData = new TextFileTask();

        static readonly ITaskData taskData = new DbTaskData();
        static readonly DbUserManager userManager = new DbUserManager();
        static readonly ToDoListManager toDoList = new(taskData);

        static void Main(string[] _)
        {
            Console.WriteLine("=== Welcome to Your To-Do List System ===");
            Console.WriteLine("1. Login\n2. Register");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                if (Login())
                    ShowMenu();
                else
                    Console.WriteLine("Invalid login. Exiting...");
            }
            else if (choice == "2")
            {
                Register();
                if (!string.IsNullOrWhiteSpace(currentUser))
                    ShowMenu();
                else
                    Console.WriteLine("Registration failed.");
            }
            else
            {
                Console.WriteLine("Invalid choice. Exiting...");
            }
        }

        static bool Login()
        {
            Console.Write("Enter Username: ");
            string? username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string? password = Console.ReadLine();

            if (userManager.Authenticate(username, password))
            {
                currentUser = username;
                return true;
            }
            return false;
        }

        static void Register()
        {
            Console.Write("Create Username: ");
            string? username = Console.ReadLine();

            Console.Write("Create Password: ");
            string? password = Console.ReadLine();

            if (userManager.Register(username, password))
            {
                Console.WriteLine("Registration successful!");
                currentUser = username; 
            }
            else
            {
                Console.WriteLine("Username exists. Try again.");
            }
        }

        static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. View Tasks");
                Console.WriteLine("2. Add Task");
                Console.WriteLine("3. Edit Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Mark as Done");
                Console.WriteLine("6. Search Tasks");
                Console.WriteLine("7. Logout");
                Console.Write("Your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": DisplayTasks(); break;
                    case "2": AddTask(); break;
                    case "3": EditTask(); break;
                    case "4": DeleteTask(); break;
                    case "5": MarkAsDone(); break;
                    case "6": SearchTask(); break;
                    case "7": currentUser = null; return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void DisplayTasks()
        {
            var tasks = toDoList.GetTasks(currentUser);
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task description: ");
            string? desc = Console.ReadLine();
            toDoList.AddTask(currentUser, desc);
            Console.WriteLine("Task added.");
        }

        static void EditTask()
        {
            DisplayTasks();
            Console.Write("Enter task number to edit: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                Console.Write("New description: ");
                string? newDesc = Console.ReadLine();
                if (toDoList.EditTask(index - 1, newDesc, currentUser))
                    Console.WriteLine("Updated successfully.");
                else
                    Console.WriteLine("Failed to update.");
            }
        }

        static void DeleteTask()
        {
            DisplayTasks();
            Console.Write("Enter task number to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (toDoList.DeleteTask(index - 1, currentUser))
                    Console.WriteLine("Deleted successfully.");
                else
                    Console.WriteLine("Failed to delete.");
            }
        }

        static void MarkAsDone()
        {
            DisplayTasks();
            Console.Write("Enter task number to mark as done: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (toDoList.MarkAsDone(index - 1, currentUser))
                    Console.WriteLine("Marked as done.");
                else
                    Console.WriteLine("Failed to mark as done.");
            }
        }

        static void SearchTask()
        {
            Console.Write("Enter keyword: ");
            string? keyword = Console.ReadLine();
            var results = toDoList.SearchTasks(keyword, currentUser);
            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {results[i]}");
            }
        }
    }
}
