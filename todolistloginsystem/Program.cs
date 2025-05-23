using System;
using ToDoListProcess.BL;
using ToDoListProcess.DL;

namespace ToDoListLoginSystem
{
    internal class Program
    {
        static ToDoListManager toDoList = new();
        static IUserAccess userAccess = new TextFileUserAccess();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to Your To-Do List ===");

            string username, password;
            bool isAuthenticated = false;

            do
            {
                Console.Write("Username: ");
                username = Console.ReadLine();

                Console.Write("Password: ");
                password = Console.ReadLine();

                isAuthenticated = userAccess.ValidateCredentials(username, password);

                if (!isAuthenticated)
                    Console.WriteLine("Invalid credentials. Please try again.\n");
            }
            while (!isAuthenticated);

            Console.WriteLine($"Welcome {username}, to your To-Do List!");
            ShowMenu();
        }

        static void ShowMenu() { /* keep original menu methods */ }
    }
}