using Northwind.Dal.EntityFramework.Models;

namespace Northwind.Dal.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Northwind.Dal.EntityFramework.NorthwindDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Northwind.Dal.EntityFramework.NorthwindDbContext context)
        {
            context.Categories.AddOrUpdate(
                new Category()
                {
                    CategoryID = 1,
                    CategoryName = "Categoty 1",
                    Description = "Description for category 1",
                },
                new Category()
                {
                    CategoryID = 2,
                    CategoryName = "Categoty 3",
                    Description = "Description for category 2",
                });

            context.Regions.AddOrUpdate(
                new Region()
                {
                    RegionID = 1,
                    RegionDescription = "Minsk",
                },
                new Region()
                {
                    RegionID = 2,
                    RegionDescription = "Brest",
                });

            context.Territories.AddOrUpdate(
                new Territory()
                {
                    TerritoryID = "01244",
                    TerritoryDescription = "Volozin",
                    RegionID = 1,
                },
                new Territory()
                {
                    TerritoryID = "45235",
                    TerritoryDescription = "Orsha",
                    RegionID = 1,
                },
                new Territory()
                {
                    TerritoryID = "45789",
                    TerritoryDescription = "Pinsk",
                    RegionID = 2,
                },
                new Territory()
                {
                    TerritoryID = "71346",
                    TerritoryDescription = "Gancevichi",
                    RegionID = 2,
                });
        }
    }
}
