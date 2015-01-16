namespace Fiszki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Packages", "AuthorID", c => c.String());
            AlterColumn("dbo.LearnStatus", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LearnStatus", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Packages", "AuthorID", c => c.Int(nullable: false));
        }
    }
}
