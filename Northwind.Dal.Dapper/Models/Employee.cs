using System.Collections.Generic;

namespace Northwind.Dal.Dapper.Models
{
    public class Employee
    {
        public Employee()
        {
            Regions = new List<Region>();
            Shippers = new List<Shipper>();
        }

        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Region> Regions { get; }

        public ICollection<Shipper> Shippers { get; }
    }
}
