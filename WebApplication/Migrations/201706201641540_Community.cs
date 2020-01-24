namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Community : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CatagoryName = c.String(),
                        NumOfForums = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Forum",
                c => new
                    {
                        ForumID = c.Int(nullable: false, identity: true),
                        ForumName = c.String(),
                        ForumDescription = c.String(),
                        NumOfTopics = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ForumID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: false)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicID = c.Int(nullable: false, identity: true),
                        TopicTitle = c.String(),
                        UserID = c.Int(nullable: false),
                        Content = c.String(),
                        NumOfComments = c.Int(nullable: false),
                        ForumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopicID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .ForeignKey("dbo.Forum", t => t.ForumID, cascadeDelete: false)
                .Index(t => t.UserID)
                .Index(t => t.ForumID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        Email = c.String(),
                        Avatar = c.String(),
                        NumOfTopics = c.Int(nullable: false),
                        NumOfComments = c.Int(nullable: false),
                        UserGroup = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        UserID = c.Int(nullable: false),
                        TopicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.TopicID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "ForumID", "dbo.Forum");
            DropForeignKey("dbo.Topics", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.Forum", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Comments", new[] { "TopicID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Topics", new[] { "ForumID" });
            DropIndex("dbo.Topics", new[] { "UserID" });
            DropIndex("dbo.Forum", new[] { "CategoryID" });
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Topics");
            DropTable("dbo.Forum");
            DropTable("dbo.Categories");
        }
    }
}
