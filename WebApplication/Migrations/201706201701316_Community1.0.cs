namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Community10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropIndex("dbo.Topics", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            AlterColumn("dbo.Topics", "UserID", c => c.Int());
            AlterColumn("dbo.Comments", "UserID", c => c.Int());
            CreateIndex("dbo.Topics", "UserID");
            CreateIndex("dbo.Comments", "UserID");
            AddForeignKey("dbo.Topics", "UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Comments", "UserID", "dbo.Users", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Topics", "UserID", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Topics", new[] { "UserID" });
            AlterColumn("dbo.Comments", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Topics", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "UserID");
            CreateIndex("dbo.Topics", "UserID");
            AddForeignKey("dbo.Comments", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Topics", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}
