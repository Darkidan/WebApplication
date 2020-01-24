namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Community12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "UserID", "dbo.Users");
            DropIndex("dbo.Topics", new[] { "UserID" });
            AlterColumn("dbo.Topics", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Topics", "UserID");
            AddForeignKey("dbo.Topics", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "UserID", "dbo.Users");
            DropIndex("dbo.Topics", new[] { "UserID" });
            AlterColumn("dbo.Topics", "UserID", c => c.Int());
            CreateIndex("dbo.Topics", "UserID");
            AddForeignKey("dbo.Topics", "UserID", "dbo.Users", "UserID");
        }
    }
}
