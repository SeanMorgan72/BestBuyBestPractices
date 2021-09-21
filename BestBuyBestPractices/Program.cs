using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BestBuyBestPractices
{
    class Program
    {
        static IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        static string connString = config.GetConnectionString("DefaultConnection");

        static IDbConnection conn = new MySqlConnection(connString);

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of the choice you need to perform today.");
            Console.WriteLine("1. List of Departments");
            Console.WriteLine("2. Create a New Department");
            Console.WriteLine("3. List of Products");
            Console.WriteLine("4. Create a New Product");
            Console.WriteLine("5. Update Product Name");
            Console.WriteLine("6. Like to Delete a Product");
            Console.WriteLine("Enter choice: ");
            var choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ListDepartment();
                    break;

                case 2:
                    NewDepartment();
                    break;

                case 3:
                    ListProducts();
                    break;

                case 4:
                    NewProducts();
                    break;

                case 5:
                    UpdateProduct();
                    break;

                case 6:
                    DeleteProduct();
                    break;
            }                       
        }

        private static void DeleteProduct()
        {
            var repo = new DapperProductRepository(conn);

            Console.WriteLine($"Please enter the productID of the product you would like to delete:");
            var productID = int.Parse(Console.ReadLine());

            repo.DeleteProduct(productID);
        }

        public static void UpdateProduct()
        {
            var repo = new DapperProductRepository(conn);

            Console.WriteLine($"What is the productID of the product you would like to update?");
            var productID = int.Parse(Console.ReadLine());

            Console.WriteLine($"What is the new name you would like for the product with an id of {productID}?");
            var productName = Console.ReadLine();

            repo.UpdateProductName(productID, productName);
        }

        public static void ListDepartment()
        {
            var repo = new DapperDepartmentRepository(conn);

            var departments = repo.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine($"Department #    Department Name ");
                Console.WriteLine($"{dept.DepartmentID}\t\t {dept.Name}");
                Console.WriteLine($"--------------------------------");
            }
        }              

        public static void NewDepartment()
        {
            var repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Enter a new Department name: ");
            var newDept = Console.ReadLine();

            repo.InsertDepartment(newDept);
        }

        public static void ListProducts()
        {
            var repo = new DapperProductRepository(conn);

            var products = repo.GetAllProducts();

            foreach (var prod in products)
            {
                Console.WriteLine($"Product ID:{prod.Name}\t Product Name: {prod.Price}\t Product Price: {prod.CategoryID}");
                Console.WriteLine($"-------------------------------------------------------------------------------------");
            }
        }

        public static void NewProducts()
        {
            var repo = new DapperProductRepository(conn);

            Console.WriteLine("Enter Product Name: ");
            Console.WriteLine("Enter Product Price: ");
            Console.WriteLine("Enter Product Category ID: ");
            var newProduct = Console.ReadLine();
            var newPrice = decimal.Parse(Console.ReadLine());
            var newCat = int.Parse(Console.ReadLine());

            repo.CreateProduct(newProduct, newPrice, newCat);
        }
    }
}
