using System;
using ToDoListProcess.BL;
using ToDoListProcess.Business;
using ToDoListProcess.Common;
using ToDoListProcess.Data;

namespace ToDoListUI
{
    internal class Program
    {
        static string? currentUser = null;

        static readonly ITaskData taskData = new JsonFileTask();
        static readonly ToDoListManager toDoList = new(taskData);
        static readonly UserManager userManager = new();

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
                Console.WriteLine("You can now log in.");
                if (Login())
                    ShowMenu();
                else
                    Console.WriteLine("Invalid login after registration.");
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

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username or password cannot be empty.");
                return false;
            }

            if (userManager.Authenticate(username!, password!))
            {
                currentUser = username;
                return true;
            }
            return false;
        }

        static void Register()
        {
            Console.Write("New Username: ");
            string? username = Console.ReadLine();

            Console.Write("New Password: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and Password cannot be empty.");
                return;
            }

            if (userManager.Register(username!, password!))
                Console.WriteLine("Registration successful!");
            else
                Console.WriteLine("Username exists. Try again.");
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
                    case "7": currentUser = null; Console.WriteLine("Logged out."); return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void DisplayTasks()
        {
            if (currentUser == null)
            {
                Console.WriteLine("No user logged in.");
                return;
            }
            var tasks = toDoList.GetTasks(currentUser);
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                int i = 1;
                foreach (var task in tasks)
                {
                    Console.WriteLine($"{i}. {task}");
                    i++;
                }
            }
        }

        static void AddTask()
        {
            if (currentUser == null)
            {
                Console.WriteLine("No user logged in.");
                return;
            }
            Console.Write("Enter task description: ");
            string? desc = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(desc))
            {
                Console.WriteLine("Task description cannot be empty.");
                return;
            }
            toDoList.AddTask(currentUser, desc);
            Console.WriteLine("Task added.");
        }

        static void EditTask()
        {
            if (currentUser == null)
            {
                Console.WriteLine("No user logged in.");
                return;
            }
            DisplayTasks();
            Console.Write("Enter task number to edit: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int index))
            {
                var tasks = toDoList.GetTasks(currentUser);
                if (index < 1 || index > tasks.Count)
                {
                    Console.WriteLine("Invalid task number.");
                    return;
                }
                Console.Write("New description: ");
                string? newDesc = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newDesc))
                {
                    Console.WriteLine("Description cannot be empty.");
                    return;
                }
                if (toDoList.EditTask(index - 1, newDesc, currentUser))
                    Console.WriteLine("Updated successfully.");
                else
                    Console.WriteLine("Failed to update.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void DeleteTask()
        {
            if (currentUser == null)
            {
                Console.WriteLine("No user logged in.");
                return;
            }
            DisplayTasks();
            Console.Write("Enter task number to delete: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int index))
            {
                var tasks = toDoList.GetTasks(currentUser);
                if (index < 1 || index > tasks.Count)
                {
                    Console.WriteLine("Invalid task number.");
                    return;
                }
                if (toDoList.DeleteTask(index - 1, currentUser))
                    Console.WriteLine("Deleted successfully.");
                else
                    Console.WriteLine("Failed to delete.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void MarkAsDone()
        {
            if (currentUser == null)
            {
                Console.WriteLine("No user logged in.");
                return;
            }
            DisplayTasks();
            Console.Write("Enter task number to mark as done: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int index))
            {
                var tasks = toDoList.GetTasks(currentUser);
                if (index < 1 || index > tasks.Count)
                {
                    Console.WriteLine("Invalid task number.");
                    return;
                }
                if (toDoList.MarkAsDone(index - 1, currentUser))
                    Console.WriteLine("Marked as done.");
                else
                    Console.WriteLine("Failed to mark as done.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void SearchTask()
        {
            if (currentUser == null)
            {
                Console.WriteLine("No user logged in.");
                return;
            }
            Console.Write("Enter keyword: ");
            string? keyword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Keyword cannot be empty.");
                return;
            }

            var results = toDoList.SearchTasks(keyword, currentUser);
            if (results.Count > 0)
            {
                Console.WriteLine("Search results:");
                int i = 1;
                foreach (var task in results)
                {
                    Console.WriteLine($"{i}. {task}");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("No tasks found.");
            }
        }
    }
}