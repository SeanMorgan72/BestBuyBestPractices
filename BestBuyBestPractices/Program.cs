using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperDepartmentRepository(conn);

            var departments = repo.GetAllDepartments();

            foreach(var dept in departments)
            {
                Console.WriteLine($"Department #    Department Name ");
                Console.WriteLine($"{dept.DepartmentID}\t\t {dept.Name}");
                Console.WriteLine($"--------------------------------");
            }
        }
    }
}
