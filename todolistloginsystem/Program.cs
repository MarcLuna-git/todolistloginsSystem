using System;
using ToDoListProcess;

namespace todolistloginsystem
{
    internal class Program
    {
        static ToDoListManager toDoList = new ToDoListManager(); // calling Task Manager in business and data logic

        //UI LOGIC
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to Your To-Do List ===");

            Console.Write("Enter your Name: ");
            string name = Console.ReadLine();


            string pin;
            do
            {
                Console.Write("Enter your PIN: ");
                pin = Console.ReadLine();
                if (pin != ToDoListManager.PinCode) // calling pincode from ToDoListManager which nadeclare ko na sya sa businessData
                    Console.WriteLine("Incorrect PIN! Please try again.");
            } while (pin != ToDoListManager.PinCode); // Loop until the correct PIN is entered

            Console.WriteLine($"Welcome {name}, To: To-Do List System!");
            ShowMenu();
        }

        static void ShowMenu() //shows menu options
        {
            while (true)
            {
                Console.WriteLine("\nChoices: ");
                Console.WriteLine("1. View Tasks\n2. Add Task\n3. Edit Task\n4. Delete Task\n5. Mark as Done\n6. Exit");
                Console.Write("Choose a number: ");

                switch (Console.ReadLine())
                {
                    case "1": DisplayTasks(); break;
                    case "2": AddTask(); break;
                    case "3": EditTask(); break;
                    case "4": DeleteTask(); break;
                    case "5": MarkAsDone(); break;
                    case "6":
                        Console.WriteLine("Thank you for using the To-Do List System!");
                        return;
                    default:
                        Console.WriteLine("Invalid input! Please choose between 1-6.");
                        break;
                }
            }
        }

        static void DisplayTasks() //displaying task
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
            Console.WriteLine("Task added");
        }

        static void EditTask()
        {
            DisplayTasks();
            Console.Write("Enter Task Number to Edit: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                Console.Write(" Enter new task description: ");
                Console.WriteLine(toDoList.EditTask(index, Console.ReadLine()) ? "Task updated successfully!" : "Invalid task number.");
            }
            else Console.WriteLine("Invalid input.");
        }

        static void DeleteTask()
        {
            DisplayTasks();
            Console.Write("Enter Task Number to Delete: ");
            if (int.TryParse(Console.ReadLine(), out int index))
                Console.WriteLine(toDoList.DeleteTask(index) ? "Task deleted successfully!" : "Invalid task number.");
            else Console.WriteLine("Invalid input.");
        }

        static void MarkAsDone()
        {
            DisplayTasks();
            Console.Write("Enter Task Number to Mark as Done: ");
            if (int.TryParse(Console.ReadLine(), out int index))
                Console.WriteLine(toDoList.MarkAsDone(index) ? "√ Task marked as done." : "Invalid task number.");
            else Console.WriteLine("Invalid input.");
        }
    }
}