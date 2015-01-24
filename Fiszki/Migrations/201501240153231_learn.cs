namespace Fiszki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class learn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LearnStatus", "CardID", "dbo.Cards");
            DropPrimaryKey("dbo.LearnStatus");
            AddColumn("dbo.LearnStatus", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.LearnStatus", "ID");
            AddForeignKey("dbo.LearnStatus", "CardID", "dbo.Cards", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LearnStatus", "CardID", "dbo.Cards");
            DropPrimaryKey("dbo.LearnStatus");
            DropColumn("dbo.LearnStatus", "ID");
            AddPrimaryKey("dbo.LearnStatus", "CardID");
            AddForeignKey("dbo.LearnStatus", "CardID", "dbo.Cards", "ID");
        }
    }
}
