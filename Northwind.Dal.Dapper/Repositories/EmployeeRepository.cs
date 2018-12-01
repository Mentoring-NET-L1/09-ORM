using System.Collections.Generic;
using System.Linq;
using Dapper;
using Northwind.Dal.Dapper.Models;

namespace Northwind.Dal.Dapper.Repositories
{
    public class EmployeeRepository : Repository
    {
        public EmployeeRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        public IEnumerable<Employee> GetWithRegions()
        {
            var employees = GetManyToMany<Employee, Region>(
                SqlScripts.EmployeeRepository.Queries.GetWithRegions,
                employee => employee.EmployeeID,
                region => region.RegionID,
                employee => employee.Regions);

            foreach (var region in employees.SelectMany(employee => employee.Regions))
            {
                region.RegionDescription = region.RegionDescription.Trim();
            }

            return employees;
        }

        public IEnumerable<Employee> GetWithShippers()
        {
            return ExecuteCommand((connection) =>
            {
                return GetManyToMany<Employee, Shipper>(
                    SqlScripts.EmployeeRepository.Queries.GetWithShippers,
                    employee => employee.EmployeeID,
                    shipper => shipper.ShipperID,
                    employee => employee.Shippers);
            });
        }

        public Employee Add(Employee employee, IEnumerable<string> territories)
        {
            return ExecuteCommand((connection) =>
            {
                var addedEmployee = connection.QueryFirst<Employee>(SqlScripts.EmployeeRepository.Queries.Add, employee);
                connection.Execute(
                    SqlScripts.EmployeeRepository.Queries.AddEmployeeTerritory,
                    territories.Select(territoryID => new
                    {
                        EmployeeID = addedEmployee.EmployeeID,
                        TerritoryID = territoryID
                    }));

                return addedEmployee;
            });
        }
    }
}
