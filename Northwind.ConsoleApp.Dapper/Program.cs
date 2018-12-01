using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Northwind.Dal.Dapper.Models;
using Northwind.Dal.Dapper.Repositories;

namespace Northwind.ConsoleApp.Dapper
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var connectionSettings = GetConnectionSettings("Northwind");

                var productRepository = new ProductRepository(connectionSettings.ConnectionString, connectionSettings.ProviderName);
                foreach (var product in productRepository.Get())
                {
                    Console.WriteLine($"{product.Category.CategoryName}, {product.ProductName}, {product.Supplier.CompanyName}");
                }

                productRepository.ReplaceToCategory(1, 1);

                productRepository.Add(new []
                {
                    new Product
                    {
                        ProductName = "Product2",
                        UnitPrice = 100500,
                        Category = new Category
                        {
                            CategoryName = "MyCategory",
                            Description = "Some Description",
                        },
                        Supplier = new Supplier
                        {
                            CompanyName = "SupplierCompany",
                            ContactName = "Contact",
                        },
                    }, 
                });

                var emploeeRepository = new EmployeeRepository(connectionSettings.ConnectionString, connectionSettings.ProviderName);

                //emploeeRepository.Add(new Employee(){FirstName = "Sergey", LastName = "Pashkovski"}, new[] {"32859", "40222"});

                foreach (var employee in emploeeRepository.GetWithRegions())
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName}: {string.Join(", ", employee.Regions.Select(t => t.RegionDescription))}");
                }

                //var emploeeRepository = new EmployeeRepository(connectionSettings.ConnectionString, connectionSettings.ProviderName);
                //foreach (var employee in emploeeRepository.GetWithShippers())
                //{
                //    Console.WriteLine($"{employee.FirstName} {employee.LastName}: {string.Join(", ", employee.Shippers.Select(t => t.CompanyName))}");
                //}

                var statistics = new Statistics(connectionSettings.ConnectionString, connectionSettings.ProviderName);
                foreach (var item in statistics.EmployeeCountInRegions())
                {
                    Console.WriteLine($"{item.Region.RegionDescription}: {item.EmployeeCount}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        private static ConnectionStringSettings GetConnectionSettings(string name)
        {
            var settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings connectionSettings in settings)
                {
                    if (connectionSettings.Name == name)
                        return connectionSettings;
                }
            }

            throw new KeyNotFoundException($"Can't find connection string with name \"{name}\".");
        }
    }
}
