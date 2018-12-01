using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Northwind.Dal.Dapper.Models;

namespace Northwind.Dal.Dapper.Repositories
{
    public abstract class Repository
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        protected Repository(string connectionString, string provider)
        {
            _providerFactory = DbProviderFactories.GetFactory(provider);
            _connectionString = connectionString;
        }

        protected TResult ExecuteCommand<TResult>(Func<DbConnection, TResult> execute)
        {
            TResult result;

            using (var connection = _providerFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                result = execute(connection);
            }

            return result;
        }

        protected IList<TPrimaryEntity> GetManyToMany<TPrimaryEntity, TSecondaryEntity>(
            string sql,
            Func<TPrimaryEntity, int> primaryIdSelector,
            Func<TSecondaryEntity, int> secondaryIdSelector,
            Func<TPrimaryEntity, ICollection<TSecondaryEntity>> collectionSelector)
        {
            return ExecuteCommand((connection) =>
            {
                using (var multi = connection.QueryMultiple(sql))
                {
                    var primaryEntities = multi.Read<TPrimaryEntity>().ToList();
                    var manyToMany = multi.Read<ManyToMany>().ToList();
                    var secondaryEntities = multi.Read<TSecondaryEntity>().ToList();

                    foreach (var primaryEntity in primaryEntities)
                    {
                        foreach (var manyToManyItem in manyToMany
                            .Where(m2m => m2m.PrimaryID == primaryIdSelector(primaryEntity)))
                        {
                            collectionSelector(primaryEntity).Add(secondaryEntities.FirstOrDefault(
                                secondaryEntity => secondaryIdSelector(secondaryEntity) == manyToManyItem.SecondaryID));
                        }
                    }

                    return primaryEntities;
                }
            });
        }
    }
}
