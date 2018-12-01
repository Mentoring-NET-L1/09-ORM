namespace Northwind.Dal.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_1_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        CardID = c.Int(nullable: false),
                        Number = c.String(nullable: false, maxLength: 16),
                        ExpirationDate = c.DateTime(nullable: false),
                        CardHolder = c.String(nullable: false, maxLength: 50),
                        Employee_EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeID)
                .Index(t => t.Employee_EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "Employee_EmployeeID", "dbo.Employees");
            DropIndex("dbo.Cards", new[] { "Employee_EmployeeID" });
            DropTable("dbo.Cards");
        }
    }
}
