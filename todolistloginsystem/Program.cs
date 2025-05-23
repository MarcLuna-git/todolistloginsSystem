using System;
using ToDoListProcess.BL;
using ToDoListProcess.Common;
using ToDoListProcess.DL;

namespace ToDoListLoginSystem
{
    internal class Program
    {
        static ToDoListManager toDoList = new ToDoListManager(new InMemoryTask()); // Switchable sya to JsonFileTask or TextFileTask

        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to Your To-Do List Login System ===");

            Console.Write("Enter your Name: ");
            string name = Console.ReadLine();

            Console.WriteLine($"Welcome {name}, To: To-Do List System!");
            ShowMenu();
        }

        static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nChoices: ");
                Console.WriteLine("1. View Tasks\n2. Add Task\n3. Edit Task\n4. Delete Task\n5. Mark as Done\n6. Search Task\n7. Exit");
                Console.Write("Choose a number: ");

                switch (Console.ReadLine())
                {
                    case "1": DisplayTasks(); break;
                    case "2": AddTask(); break;
                    case "3": EditTask(); break;
                    case "4": DeleteTask(); break;
                    case "5": MarkAsDone(); break;
                    case "6": SearchTask(); break;
                    case "7":
                        Console.WriteLine("Thank you for using the To-Do List System!");
                        return;
                    default:
                        Console.WriteLine("Invalid input! Please choose between 1-7.");
                        break;
                }
            }
        }

        static void DisplayTasks()
        {
            var tasks = toDoList.GetTasks();
            Console.WriteLine(tasks.Count == 0 ? "No tasks available! Add one to get started." : "Your Current Tasks:");
            for (int i = 0; i < tasks.Count; i++)
                Console.WriteLine($"{i + 1}. {tasks[i]}");
        }

        static void AddTask()
        {
            Console.Write("Enter Task: ");
            toDoList.AddTask(Console.ReadLine());
            Console.WriteLine("Task added successfully.");
        }

        static void EditTask()
        {
            DisplayTasks();
            Console.Write("Enter Task Number to Edit: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index -= 1;
                Console.Write("Enter new task description: ");
                string newDescription = Console.ReadLine();
                Console.WriteLine(toDoList.EditTask(index, newDescription) ? "Task updated successfully!" : "Failed to update task.");
            }
            else Console.WriteLine("Invalid input.");
        }

        static void DeleteTask()
        {
            DisplayTasks();
            Console.Write("Enter Task Number to Delete: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index -= 1;
                Console.WriteLine(toDoList.DeleteTask(index) ? "Task deleted successfully!" : "Failed to delete task.");
            }
            else Console.WriteLine("Invalid input.");
        }

        static void MarkAsDone()
        {
            DisplayTasks();
            Console.Write("Enter Task Number to Mark as Done: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index -= 1;
                Console.WriteLine(toDoList.MarkAsDone(index) ? "√ Task marked as done." : "Failed to mark task as done.");
            }
            else Console.WriteLine("Invalid input.");
        }

        static void SearchTask()
        {
            Console.Write("Enter keyword to search for a task: ");
            var tasks = toDoList.SearchTasks(Console.ReadLine());
            if (tasks.Count > 0)
                foreach (var task in tasks) Console.WriteLine(task);
            else Console.WriteLine("No tasks found matching the keyword.");
        }
    }
}
