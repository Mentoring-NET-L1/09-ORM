using System.Collections.Generic;
using System.Linq;
using Dapper;
using Northwind.Dal.Dapper.Models;

namespace Northwind.Dal.Dapper.Repositories
{
    public class ProductRepository : Repository
    {
        public ProductRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        public IEnumerable<Product> Get()
        {
            return ExecuteCommand((connection) =>
            {
                return connection.Query<Product, Category, Supplier, Product>(
                    SqlScripts.ProductRepository.Queries.Get,
                    (product, category, supplier) =>
                    {
                        product.Category = category;
                        product.Supplier = supplier;
                        return product;
                    },
                    splitOn: "CategoryID, SupplierID");
            });
        }

        public void ReplaceToCategory(int productID, int categoryID)
        {
            ExecuteCommand((connection) => connection.Execute(
                SqlScripts.ProductRepository.Queries.ReplaceToCategory,
                new { ProductID = productID, CategoryID = categoryID }));
        }

        public void Add(IEnumerable<Product> products)
        {
            ExecuteCommand((connection) =>
            {
                return connection.Execute(
                    SqlScripts.ProductRepository.Queries.Add,
                    products.Select(product => new
                    {
                        ProductName = product.ProductName,
                        UnitPrice = product.UnitPrice,
                        Discontinued = product.Discontinued,
                        CategoryName = product.Category?.CategoryName,
                        CategoryDescription = product.Category?.Description,
                        SupplierName = product.Supplier?.CompanyName,
                        SupplierContactName = product.Supplier?.ContactName,
                    }));
            });
        }
    }
}
