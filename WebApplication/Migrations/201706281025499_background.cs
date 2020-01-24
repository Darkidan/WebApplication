namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class background : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fora", "background", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fora", "background");
        }
    }
}
