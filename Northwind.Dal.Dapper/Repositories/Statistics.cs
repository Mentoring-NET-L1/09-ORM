using System.Collections.Generic;
using Dapper;
using Northwind.Dal.Dapper.Models;
using Northwind.Dal.Dapper.Models.Statistics;

namespace Northwind.Dal.Dapper.Repositories
{
    public class Statistics : Repository
    {
        public Statistics(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        public IEnumerable<EmployeeCountInRegion> EmployeeCountInRegions()
        {
            return ExecuteCommand((connection) =>
            {
                return connection.Query<Region, EmployeeCountInRegion, EmployeeCountInRegion>(
                    SqlScripts.Statistics.Queries.EmployeeCountInRegions,
                    (region, employeeCountInRegion) =>
                    {
                        employeeCountInRegion.Region = region;
                        return employeeCountInRegion;
                    },
                    splitOn: "EmployeeCount");
            });
        }
    }
}
