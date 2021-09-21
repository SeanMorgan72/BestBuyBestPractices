using System;
using System.Collections.Generic;

namespace BestBuyBestPractices
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(string name, decimal price, int categoryID);
    }
}
