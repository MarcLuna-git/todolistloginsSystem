using System;
namespace todolistloginsystem
{
    internal class Program
    {
        static List<string> tasks = new List<string>();
        static int pinCode = 2004;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to Your To-Do List ===");
            Console.Write("Enter your Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your PIN: ");
            int pin = int.Parse(Console.ReadLine());

            if (pin == pinCode)
            {
                Console.WriteLine("=============================");
                Console.WriteLine($"Welcome {name},To: To Do-List System!");
                ShowMenu();
            }
            else
            {
                Console.WriteLine("Wrong PIN");
            }
        }
        //methods that Showing Task Menu 
        static void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nChoices: ");
                Console.WriteLine("1 View Tasks");
                Console.WriteLine("2️ Add Task");
                Console.WriteLine("3️ Edit Task");
                Console.WriteLine("4️ Delete Task");
                Console.WriteLine("5️ Mark as Done");
                Console.WriteLine("6️ Exit");
                Console.WriteLine("====================");
                Console.Write("Choose a number: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewTasks();
                        break;
                    case "2":
                        AddTask();
                        break;
                    case "3":
                        EditTask();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        MarkAsDone();
                        break;
                    case "6":
                        exit = true;
                        Console.WriteLine("Thankyou!");
                        break;
                    default:
                        Console.WriteLine("Please Input 1-6 only.");
                        break;
                }
            }
        }
        //methods to view your Task
        static void ViewTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks Available! Add one to get started.");
            }
            else
            {
                Console.WriteLine("Your Current Task: ");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
            }
        }

        //methods to Add more Task
        static void AddTask()
        {
            Console.Write("Add Task: ");
            string newTask = Console.ReadLine();
            tasks.Add(newTask);
            Console.WriteLine("Task added!");
        }

        //methods that you want to edit your task
        static void EditTask()
        {
            ViewTasks();
            Console.Write("Enter Task Number To Edit. ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index >= 0 && index < tasks.Count)
            {
                Console.Write("Enter the new task description: ");
                tasks[index] = Console.ReadLine();
                Console.WriteLine("Task Updated!");
            }
            else
            {
                Console.WriteLine("Invalid Task number.");
            }
        }

        //methods that you want to delete task
        static void DeleteTask()
        {
            ViewTasks();
            Console.Write("Select Task that want you to delete. ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                Console.WriteLine("Task deleted.");
            }
            else
            {
                Console.WriteLine("That task number doesn't exist.");
            }
        }

        //methods that you want to mark as done or completed
        static void MarkAsDone()
        {
            ViewTasks();
            Console.Write("Select Task number to mark as done: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index >= 0 && index < tasks.Count)
            {
                tasks[index] = "√ " + tasks[index];
                Console.WriteLine("Thanks. Task marked as done.");
            }
            else
            {
                Console.WriteLine("That task number doesn't exist.");
            }
        }
    }
}
