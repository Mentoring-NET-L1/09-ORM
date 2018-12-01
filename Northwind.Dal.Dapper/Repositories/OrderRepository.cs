using Dapper;

namespace Northwind.Dal.Dapper.Repositories
{
    public class OrderRepository : Repository
    {
        public OrderRepository(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }

        public void ChangeProductInUnexecutedOrders(int oldProductID, int newProductID)
        {
            ExecuteCommand((connection) => connection.Execute(
                SqlScripts.OrderRepository.Queries.ChangeProductInUnexecutedOrders,
                new {OldProductID = oldProductID, NewProductID = newProductID}));
        }
    }
}
