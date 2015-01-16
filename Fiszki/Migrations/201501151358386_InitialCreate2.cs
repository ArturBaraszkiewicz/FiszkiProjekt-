namespace Fiszki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "Package_ID", "dbo.Packages");
            DropIndex("dbo.Cards", new[] { "Package_ID" });
            RenameColumn(table: "dbo.Cards", name: "Package_ID", newName: "PackageID");
            AlterColumn("dbo.Cards", "PackageID", c => c.Int(nullable: false));
            CreateIndex("dbo.Cards", "PackageID");
            AddForeignKey("dbo.Cards", "PackageID", "dbo.Packages", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "PackageID", "dbo.Packages");
            DropIndex("dbo.Cards", new[] { "PackageID" });
            AlterColumn("dbo.Cards", "PackageID", c => c.Int());
            RenameColumn(table: "dbo.Cards", name: "PackageID", newName: "Package_ID");
            CreateIndex("dbo.Cards", "Package_ID");
            AddForeignKey("dbo.Cards", "Package_ID", "dbo.Packages", "ID");
        }
    }
}
