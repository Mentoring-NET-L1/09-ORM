using System;
using System.Linq;
using Northwind.Dal.EntityFramework;

namespace Northwind.ConsoleApp.EntityFramework
{
    internal class Program
    {
        private static void Main()
        {
            using (var context = new NorthwindDbContext())
            {
                var categoryName = "Beverages";
                var categoryOrders =  context.Orders
                    .Where(order => order.Order_Details.Any(od => od.Product.Category.CategoryName == categoryName));
                foreach (var order in categoryOrders)
                {
                    Console.WriteLine($"{order.Customer.CompanyName}: {string.Join(", ", order.Order_Details.Select(od => od.Product.ProductName))}");
                }
            }
            Console.ReadLine();
        }
    }
}
