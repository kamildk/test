namespace recenzent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Views", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Changes", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Reviews", "EditorId", "dbo.Editors");
            DropForeignKey("dbo.Reviews", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Files", "ChangeId", "dbo.Changes");
            DropForeignKey("dbo.Sources", "SourcePositionId", "dbo.SourcePositions");
            DropForeignKey("dbo.Publications", "SourcePositionId", "dbo.SourcePositions");
            DropForeignKey("dbo.Changes", "Type_Change_typeId", "dbo.Change_type");
            DropIndex("dbo.Views", new[] { "UserId" });
            DropIndex("dbo.Changes", new[] { "EmployeeId" });
            DropIndex("dbo.Changes", new[] { "Type_Change_typeId" });
            DropIndex("dbo.Reviews", new[] { "EmployeeId" });
            DropIndex("dbo.Reviews", new[] { "EditorId" });
            DropIndex("dbo.Files", new[] { "ChangeId" });
            DropIndex("dbo.Publications", new[] { "SourcePositionId" });
            DropIndex("dbo.Sources", new[] { "SourcePositionId" });
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Publication_PublicationId = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.Publication_PublicationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Publication_PublicationId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Publication_Autors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Autor_Id = c.String(nullable: false, maxLength: 128),
                        Publication_PublicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Autor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_PublicationId, cascadeDelete: true)
                .Index(t => t.Autor_Id)
                .Index(t => t.Publication_PublicationId);
            
            CreateTable(
                "dbo.PublicationTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        PublicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.PublicationId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.PublicationId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReviewStateHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChangeDate = c.DateTime(nullable: false),
                        ReviewState_Id = c.Int(),
                        ReviewState_Id1 = c.Int(),
                        ChangesFrom_Id = c.Int(nullable: false),
                        ChangesToState_Id = c.Int(nullable: false),
                        Review_ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReviewStates", t => t.ReviewState_Id)
                .ForeignKey("dbo.ReviewStates", t => t.ReviewState_Id1)
                .ForeignKey("dbo.ReviewStates", t => t.ChangesFrom_Id, cascadeDelete: false)
                .ForeignKey("dbo.ReviewStates", t => t.ChangesToState_Id, cascadeDelete: false)
                .ForeignKey("dbo.Reviews", t => t.Review_ReviewId, cascadeDelete: false)
                .Index(t => t.ReviewState_Id)
                .Index(t => t.ReviewState_Id1)
                .Index(t => t.ChangesFrom_Id)
                .Index(t => t.ChangesToState_Id)
                .Index(t => t.Review_ReviewId);
            
            CreateTable(
                "dbo.ReviewStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Changes", "File_FileId", c => c.Int());
            AddColumn("dbo.Changes", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Comments", "ParentComment_CommentId", c => c.Int());
            AddColumn("dbo.Comments", "Publication_PublicationId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Reviews", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Files", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Files", "Review_ReviewId", c => c.Int());
            AddColumn("dbo.Publications", "Description", c => c.String(nullable: false));
            AddColumn("dbo.SourcePositions", "Publication_PublicationId", c => c.Int(nullable: false));
            AddColumn("dbo.SourcePositions", "Source_SourceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Affiliations", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Changes", "Type_Change_typeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Change_type", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "Link_source", c => c.String(nullable: false));
            AlterColumn("dbo.Publication_category", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Publications", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Sources", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Changes", "File_FileId");
            CreateIndex("dbo.Changes", "User_Id");
            CreateIndex("dbo.Changes", "Type_Change_typeId");
            CreateIndex("dbo.Files", "Review_ReviewId");
            CreateIndex("dbo.Comments", "ParentComment_CommentId");
            CreateIndex("dbo.Comments", "Publication_PublicationId");
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.SourcePositions", "Publication_PublicationId");
            CreateIndex("dbo.SourcePositions", "Source_SourceId");
            CreateIndex("dbo.Reviews", "UserId");
            AddForeignKey("dbo.Changes", "File_FileId", "dbo.Files", "FileId");
            AddForeignKey("dbo.Comments", "ParentComment_CommentId", "dbo.Comments", "CommentId");
            AddForeignKey("dbo.Comments", "Publication_PublicationId", "dbo.Publications", "PublicationId", cascadeDelete: true);
            AddForeignKey("dbo.Changes", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SourcePositions", "Publication_PublicationId", "dbo.Publications", "PublicationId", cascadeDelete: true);
            AddForeignKey("dbo.SourcePositions", "Source_SourceId", "dbo.Sources", "SourceId", cascadeDelete: true);
            AddForeignKey("dbo.Files", "Review_ReviewId", "dbo.Reviews", "ReviewId");
            AddForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Changes", "Type_Change_typeId", "dbo.Change_type", "Change_typeId", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "Nick");
            DropColumn("dbo.Changes", "EmployeeId");
            DropColumn("dbo.Reviews", "IsAccepted");
            DropColumn("dbo.Reviews", "EmployeeId");
            DropColumn("dbo.Reviews", "EditorId");
            DropColumn("dbo.Files", "ChangeId");
            DropColumn("dbo.Publications", "Abstact");
            DropColumn("dbo.Publications", "SourcePositionId");
            DropColumn("dbo.Sources", "SourcePositionId");
            DropTable("dbo.Admins");
            DropTable("dbo.Views");
            DropTable("dbo.Employees");
            DropTable("dbo.Editors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Editors",
                c => new
                    {
                        EditorId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.EditorId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        ViewId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ViewId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.AdminId);
            
            AddColumn("dbo.Sources", "SourcePositionId", c => c.Int(nullable: false));
            AddColumn("dbo.Publications", "SourcePositionId", c => c.Int(nullable: false));
            AddColumn("dbo.Publications", "Abstact", c => c.String());
            AddColumn("dbo.Files", "ChangeId", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "EditorId", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "EmployeeId", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "IsAccepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Changes", "EmployeeId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Nick", c => c.String(nullable: false));
            DropForeignKey("dbo.Changes", "Type_Change_typeId", "dbo.Change_type");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReviewStateHistories", "Review_ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.ReviewStateHistories", "ChangesToState_Id", "dbo.ReviewStates");
            DropForeignKey("dbo.ReviewStateHistories", "ChangesFrom_Id", "dbo.ReviewStates");
            DropForeignKey("dbo.ReviewStateHistories", "ReviewState_Id1", "dbo.ReviewStates");
            DropForeignKey("dbo.ReviewStateHistories", "ReviewState_Id", "dbo.ReviewStates");
            DropForeignKey("dbo.Files", "Review_ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.SourcePositions", "Source_SourceId", "dbo.Sources");
            DropForeignKey("dbo.SourcePositions", "Publication_PublicationId", "dbo.Publications");
            DropForeignKey("dbo.PublicationTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PublicationTags", "PublicationId", "dbo.Publications");
            DropForeignKey("dbo.Publication_Autors", "Publication_PublicationId", "dbo.Publications");
            DropForeignKey("dbo.Publication_Autors", "Autor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "Publication_PublicationId", "dbo.Publications");
            DropForeignKey("dbo.Changes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Publication_PublicationId", "dbo.Publications");
            DropForeignKey("dbo.Comments", "ParentComment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Changes", "File_FileId", "dbo.Files");
            DropIndex("dbo.ReviewStateHistories", new[] { "Review_ReviewId" });
            DropIndex("dbo.ReviewStateHistories", new[] { "ChangesToState_Id" });
            DropIndex("dbo.ReviewStateHistories", new[] { "ChangesFrom_Id" });
            DropIndex("dbo.ReviewStateHistories", new[] { "ReviewState_Id1" });
            DropIndex("dbo.ReviewStateHistories", new[] { "ReviewState_Id" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.SourcePositions", new[] { "Source_SourceId" });
            DropIndex("dbo.SourcePositions", new[] { "Publication_PublicationId" });
            DropIndex("dbo.PublicationTags", new[] { "PublicationId" });
            DropIndex("dbo.PublicationTags", new[] { "TagId" });
            DropIndex("dbo.Publication_Autors", new[] { "Publication_PublicationId" });
            DropIndex("dbo.Publication_Autors", new[] { "Autor_Id" });
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropIndex("dbo.Ratings", new[] { "Publication_PublicationId" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Publication_PublicationId" });
            DropIndex("dbo.Comments", new[] { "ParentComment_CommentId" });
            DropIndex("dbo.Files", new[] { "Review_ReviewId" });
            DropIndex("dbo.Changes", new[] { "Type_Change_typeId" });
            DropIndex("dbo.Changes", new[] { "User_Id" });
            DropIndex("dbo.Changes", new[] { "File_FileId" });
            AlterColumn("dbo.Sources", "Name", c => c.String());
            AlterColumn("dbo.Publications", "Title", c => c.String());
            AlterColumn("dbo.Publication_category", "Name", c => c.String());
            AlterColumn("dbo.Files", "Link_source", c => c.String());
            AlterColumn("dbo.Comments", "Text", c => c.String());
            AlterColumn("dbo.Change_type", "Name", c => c.String());
            AlterColumn("dbo.Changes", "Type_Change_typeId", c => c.Int());
            AlterColumn("dbo.Affiliations", "Name", c => c.String());
            DropColumn("dbo.SourcePositions", "Source_SourceId");
            DropColumn("dbo.SourcePositions", "Publication_PublicationId");
            DropColumn("dbo.Publications", "Description");
            DropColumn("dbo.Files", "Review_ReviewId");
            DropColumn("dbo.Files", "Name");
            DropColumn("dbo.Reviews", "UserId");
            DropColumn("dbo.Comments", "User_Id");
            DropColumn("dbo.Comments", "Publication_PublicationId");
            DropColumn("dbo.Comments", "ParentComment_CommentId");
            DropColumn("dbo.Changes", "User_Id");
            DropColumn("dbo.Changes", "File_FileId");
            DropColumn("dbo.AspNetUsers", "RegistrationDate");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.ReviewStates");
            DropTable("dbo.ReviewStateHistories");
            DropTable("dbo.Tags");
            DropTable("dbo.PublicationTags");
            DropTable("dbo.Publication_Autors");
            DropTable("dbo.Ratings");
            CreateIndex("dbo.Sources", "SourcePositionId");
            CreateIndex("dbo.Publications", "SourcePositionId");
            CreateIndex("dbo.Files", "ChangeId");
            CreateIndex("dbo.Reviews", "EditorId");
            CreateIndex("dbo.Reviews", "EmployeeId");
            CreateIndex("dbo.Changes", "Type_Change_typeId");
            CreateIndex("dbo.Changes", "EmployeeId");
            CreateIndex("dbo.Views", "UserId");
            AddForeignKey("dbo.Changes", "Type_Change_typeId", "dbo.Change_type", "Change_typeId");
            AddForeignKey("dbo.Publications", "SourcePositionId", "dbo.SourcePositions", "SourcePositionId", cascadeDelete: true);
            AddForeignKey("dbo.Sources", "SourcePositionId", "dbo.SourcePositions", "SourcePositionId", cascadeDelete: true);
            AddForeignKey("dbo.Files", "ChangeId", "dbo.Changes", "ChangeId", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "EditorId", "dbo.Editors", "EditorId", cascadeDelete: true);
            AddForeignKey("dbo.Changes", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.Views", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
