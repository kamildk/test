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
            AddColumn("dbo.Publications", "ShareDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Publications", "ReviewId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Publications", "ReviewId", c => c.Int(nullable: false));
            DropColumn("dbo.Publications", "ShareDate");
            CreateIndex("dbo.Publications", "ReviewId");
            AddForeignKey("dbo.Publications", "ReviewId", "dbo.Reviews", "ReviewId", cascadeDelete: true);
        }
    }
}
