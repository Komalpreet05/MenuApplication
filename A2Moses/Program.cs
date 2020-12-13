using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;

namespace A2Moses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("  ");
            Console.WriteLine("\t\t\t\t\tAssignment 02 by Moses Oduwale");
           
            string selection = "0";
            MenuBasedApplication menuBasedApplication = new MenuBasedApplication();
            do
            {
                Console.WriteLine("  ");
                Console.WriteLine("<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>");
                Console.WriteLine("  ");
                Console.WriteLine("\t1-Get All Employees");
                Console.WriteLine("\t2-Search Employee By Name");
                Console.WriteLine("\t3-Sort Employees By Age");
                Console.WriteLine("\t4-Get all Orders");
                Console.WriteLine("\t5-Exit");
                Console.WriteLine("  ");

                Console.WriteLine("Enter your choice: ");

                selection = Console.ReadLine();
                Console.WriteLine(" ");

                switch (selection)
                {
                    case "1":
                        menuBasedApplication.PrintEmployees();
                        break;
                    case "2":
                        menuBasedApplication.GetEmployeeByName();
                        break;
                    case "3":
                        menuBasedApplication.sortByAge();
                        break;
                    case "4":
                        menuBasedApplication.GetAllOrders();
                        break;
                    case "5":
                        Console.WriteLine("Thanks for using my application!! Good Bye");
                        break;
                    default:
                        Console.WriteLine("Inavlid Selection, Please Try Again");
                        break;
                }
            }
            while (selection != "5");
        }

    }
}
