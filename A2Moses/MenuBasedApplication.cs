using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;

namespace A2Moses
{
    class MenuBasedApplication
    {
        internal MenuBasedApplication()
        {
            
        }
        internal void PrintEmployees()
        {
            string cs = GetConnectionString("NorthwindLocal");

            string query = "Select EmployeeID, FirstName, LastName, Title, BirthDate from Employees";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                Console.WriteLine("Employees Data: \n");
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine($"\t{"Emp Id",7} {"First Name",-15} {"Last Name",-15} {"Title",-25} {"Birth Date",20}");
                Console.WriteLine("\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" ");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    string title = reader.GetString(3);
                    string birthDate = reader.GetDateTime(4).ToShortDateString();
                    Console.WriteLine($"\t{id,7} {firstName,-15} {lastName,-15} {title,-25} {birthDate,20}");
                }

            }


        }
        internal void GetEmployeeByName()
        {
            string cs = GetConnectionString("NorthwindLocal");
            Console.Write("Enter employee's name: ");

            string fName = Console.ReadLine();

            Console.WriteLine(" ");
            Console.WriteLine("Related data: \n");

            string query = "Select EmployeeID, FirstName, LastName, Title, BirthDate from Employees Where FirstName LIKE '%" + fName + "%' OR LastName LIKE '%" + fName + "%'";
            using (SqlConnection conn = new SqlConnection(cs))
            {

                Console.WriteLine($"\t{"Emp Id",7} {"First Name",-15} {"Last Name",-15} {"Title",-25} {"Birth Date",20}");

                Console.WriteLine("\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" ");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("FirstName", fName);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    string title = reader.GetString(3);
                    string birthDate = reader.GetDateTime(4).ToShortDateString();
                    Console.WriteLine($"\t{id,7} {firstName,-15} {lastName,-15} {title,-25} {birthDate,20}");
                }
            }
        }

        internal void sortByAge()
        {
            string cs = GetConnectionString("NorthwindLocal");
            string query = "Select EmployeeID, FirstName, LastName, Title, BirthDate from Employees ORDER BY BirthDate";



            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                Console.WriteLine("Sorted By Age: \n");
                Console.WriteLine($"\t{"Emp ID",-7} {"First Name",-15} {"Last Name",-15} {"Title",-25} {"Birth Date",-18} { "Age ",-13} ");
                Console.WriteLine("\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" ");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    string title = reader.GetString(3);

                    DateTime birth = reader.GetDateTime(4);

                    string dob = birth.ToShortDateString();
                    int age = DateTime.Now.Year - birth.Year;

                    Console.WriteLine($"\t{id,-7} {firstName,-15} {lastName,-15} {title,-25} {dob,-18} { age + " years",-13}");

                }
                return;
            }
        }



        internal void GetAllOrders()
        {
            string cs = GetConnectionString("NorthwindLocal");
            string query = "Select Orders.OrderID, Orders.OrderDate, Orders.ShippedDate, Orders.ShipCity, Orders.ShipCountry, Employees.FirstName, Employees.Lastname from Orders INNER JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID Where Orders.OrderID <= " + 10262;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                Console.WriteLine("Orders:  \n");
                Console.WriteLine($"\t{"Order ID",-8} { "Emp Name",-20} {"Order Date",-15} {"Shipped Date",-15} {"Ship City",-15} { "Ship Country",-10}");
                Console.WriteLine("\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine(" ");

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string orderDate = reader.GetDateTime(1).ToShortDateString();
                    string shippedDate = reader.GetDateTime(2).ToShortDateString();
                    string shipCity = reader.GetString(3);
                    string shipCountry = reader.GetString(4);
                    string firstName = reader.GetString(5);
                    string lastName = reader.GetString(6);
                    string name = firstName + " " + lastName;

                    Console.WriteLine($"\t{id,-8} { name,-20} {orderDate,-15} {shippedDate,-15} {shipCity,-15} { shipCountry,-10}");

                }
                return;
            }

        }

    static string GetConnectionString(string connectionStringName)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("config.json");
            IConfiguration config = configurationBuilder.Build();
            return config["ConnectionStrings:" + connectionStringName];
        }
    }
}
