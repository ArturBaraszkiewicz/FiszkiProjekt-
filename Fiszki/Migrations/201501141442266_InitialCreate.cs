namespace Fiszki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlWord = c.String(),
                        EngWord = c.String(),
                        Difficult = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                        Package_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Packages", t => t.Package_ID)
                .Index(t => t.Package_ID);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthorID = c.Int(nullable: false),
                        Name = c.String(),
                        Text = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LearnStatus",
                c => new
                    {
                        CardID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Progress = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        NextOccurrence = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CardID)
                .ForeignKey("dbo.Cards", t => t.CardID)
                .Index(t => t.CardID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LearnStatus", "CardID", "dbo.Cards");
            DropForeignKey("dbo.Cards", "Package_ID", "dbo.Packages");
            DropIndex("dbo.LearnStatus", new[] { "CardID" });
            DropIndex("dbo.Cards", new[] { "Package_ID" });
            DropTable("dbo.LearnStatus");
            DropTable("dbo.Packages");
            DropTable("dbo.Cards");
        }
    }
}
