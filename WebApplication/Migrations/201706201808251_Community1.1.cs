namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Community11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fora", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fora", "Icon");
        }
    }
}
