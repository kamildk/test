namespace recenzent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublicationTest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Publications", "ReviewId", "dbo.Reviews");
            DropIndex("dbo.Publications", new[] { "ReviewId" });
            DropColumn("dbo.Publications", "ReviewId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Publications", "ReviewId", c => c.Int(nullable: false));
            CreateIndex("dbo.Publications", "ReviewId");
            AddForeignKey("dbo.Publications", "ReviewId", "dbo.Reviews", "ReviewId", cascadeDelete: true);
        }
    }
}
