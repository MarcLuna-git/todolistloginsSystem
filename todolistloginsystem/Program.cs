using System;
namespace todolistloginsystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("System To Do List");

            string name;
            int code = 2004;

            Console.Write("Enter Name: ");
            name = Console.ReadLine();

            Console.Write("Enter PIN: ");
            int pin = Convert.ToInt16(Console.ReadLine());

            if (pin == code)
            {
                Console.WriteLine($"Welcome, {name}!To: To-Do List Login System.");

                List<string> tasks = new List<string>();
                bool exit = false;

                while (!exit)
                {

                    string[] choices = { "[1] View Task", "[2] Edit Task", "[3] Add Task", "[4] Delete Task", "[5] Mark Done", "[6] Exit" };
                    Console.WriteLine("Choices:");

                    foreach (var choice in choices)
                    {
                        Console.WriteLine(choice);
                    }

                    Console.Write("Enter your choice: ");
                    int userChoice = Convert.ToInt16(Console.ReadLine());

                    switch (userChoice)
                    {
                        case 1:
                            if (tasks.Count == 0)
                            {
                                Console.WriteLine("No tasks available.");
                            }
                            else
                            {
                                Console.WriteLine("Current tasks:");
                                for (int i = 0; i < tasks.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                                }
                            }
                            break;

                        case 2:
                            Console.Write("Enter task number to edit: ");
                            int editIndex = Convert.ToInt16(Console.ReadLine()) - 1;

                            if (editIndex >= 0 && editIndex < tasks.Count)
                            {
                                Console.Write("Enter New Task: ");
                                tasks[editIndex] = Console.ReadLine();
                                Console.WriteLine("Task updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid task number.");
                            }
                            break;

                        case 3:
                            Console.Write("Enter task description to add: ");
                            string newTask = Console.ReadLine();
                            tasks.Add(newTask);
                            Console.WriteLine("Task added successfully.");
                            break;

                        case 4:
                            Console.Write("Enter task number to delete: ");
                            int deleteIndex = Convert.ToInt16(Console.ReadLine()) - 1;

                            if (deleteIndex >= 0 && deleteIndex < tasks.Count)
                            {
                                tasks.RemoveAt(deleteIndex);
                                Console.WriteLine("Task deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid task number.");
                            }
                            break;

                        case 5:
                            Console.Write("Enter task number to mark as done: ");
                            int markIndex = Convert.ToInt16(Console.ReadLine()) - 1;

                            if (markIndex >= 0 && markIndex < tasks.Count)
                            {
                                tasks[markIndex] = "[Done] " + tasks[markIndex];
                                Console.WriteLine("Task marked as done.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid task number.");
                            }
                            break;

                        case 6:
                            exit = true;
                            Console.WriteLine("Thankyou!");
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Your PIN is Incorrect.");
            }
        }
    }
}