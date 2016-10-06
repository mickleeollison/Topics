namespace Topics.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Name = c.String(),
                        ShortDescription = c.String(),
                        TopicID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Topic", t => t.TopicID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.TopicID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        TopicID = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TopicID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        IsEnabled = c.Boolean(nullable: false),
                        RoleID = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.UserCredentials",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        PasswordHash = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Post", "UserID", "dbo.User");
            DropForeignKey("dbo.UserCredentials", "UserID", "dbo.User");
            DropForeignKey("dbo.Post", "TopicID", "dbo.Topic");
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.Post", new[] { "UserID" });
            DropIndex("dbo.UserCredentials", new[] { "UserID" });
            DropIndex("dbo.Post", new[] { "TopicID" });
            DropTable("dbo.Role");
            DropTable("dbo.UserCredentials");
            DropTable("dbo.User");
            DropTable("dbo.Topic");
            DropTable("dbo.Post");
        }
    }
}
