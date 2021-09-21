using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, decimal price, int categoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
            new { name = name, price = price, categoryID = categoryID });
        }                

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM PRODUCTS;");
        }

        public void UpdateProductName(int productID, string productName)
        {
            _connection.Execute("UPDATE products SET Name = @productName WHERE ProductID = @productID;",
            new { updatedName = productName, productID = productID });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
            new { productID = productID });
        }
    }
}
