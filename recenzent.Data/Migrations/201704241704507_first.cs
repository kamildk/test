namespace recenzent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Affiliations",
                c => new
                    {
                        AffiliationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AffiliationId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nick = c.String(nullable: false),
                        AffiliationId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Affiliations", t => t.AffiliationId, cascadeDelete: true)
                .Index(t => t.AffiliationId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        ViewId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ViewId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Author_list",
                c => new
                    {
                        Author_listId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Author_listId);
            
            CreateTable(
                "dbo.Changes",
                c => new
                    {
                        ChangeId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Type_Change_typeId = c.Int(),
                    })
                .PrimaryKey(t => t.ChangeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Change_type", t => t.Type_Change_typeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.Type_Change_typeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Change_type",
                c => new
                    {
                        Change_typeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Change_typeId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId);
            
            CreateTable(
                "dbo.Editors",
                c => new
                    {
                        EditorId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.EditorId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        IsAccepted = c.Boolean(nullable: false),
                        Creation_date = c.DateTime(nullable: false),
                        Expiration_date = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        EditorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Editors", t => t.EditorId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.EditorId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Link_source = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
                        ChangeId = c.Int(nullable: false),
                        Publication_PublicationId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Changes", t => t.ChangeId, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_PublicationId)
                .Index(t => t.ChangeId)
                .Index(t => t.Publication_PublicationId);
            
            CreateTable(
                "dbo.Publication_category",
                c => new
                    {
                        Publication_categoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Publication_categoryId);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        PublicationId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CategoryId = c.Int(nullable: false),
                        IsShared = c.Boolean(nullable: false),
                        Abstact = c.String(),
                        SourcePositionId = c.Int(nullable: false),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PublicationId)
                .ForeignKey("dbo.Publication_category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Reviews", t => t.ReviewId, cascadeDelete: true)
                .ForeignKey("dbo.SourcePositions", t => t.SourcePositionId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SourcePositionId)
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.SourcePositions",
                c => new
                    {
                        SourcePositionId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.SourcePositionId);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        SourceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourcePositionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SourceId)
                .ForeignKey("dbo.SourcePositions", t => t.SourcePositionId, cascadeDelete: true)
                .Index(t => t.SourcePositionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TestModels",
                c => new
                    {
                        TestModelId = c.Int(nullable: false, identity: true),
                        TestString = c.String(),
                    })
                .PrimaryKey(t => t.TestModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Publications", "SourcePositionId", "dbo.SourcePositions");
            DropForeignKey("dbo.Sources", "SourcePositionId", "dbo.SourcePositions");
            DropForeignKey("dbo.Publications", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.Files", "Publication_PublicationId", "dbo.Publications");
            DropForeignKey("dbo.Publications", "CategoryId", "dbo.Publication_category");
            DropForeignKey("dbo.Files", "ChangeId", "dbo.Changes");
            DropForeignKey("dbo.Reviews", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Reviews", "EditorId", "dbo.Editors");
            DropForeignKey("dbo.Changes", "Type_Change_typeId", "dbo.Change_type");
            DropForeignKey("dbo.Changes", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Views", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AffiliationId", "dbo.Affiliations");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Sources", new[] { "SourcePositionId" });
            DropIndex("dbo.Publications", new[] { "ReviewId" });
            DropIndex("dbo.Publications", new[] { "SourcePositionId" });
            DropIndex("dbo.Publications", new[] { "CategoryId" });
            DropIndex("dbo.Files", new[] { "Publication_PublicationId" });
            DropIndex("dbo.Files", new[] { "ChangeId" });
            DropIndex("dbo.Reviews", new[] { "EditorId" });
            DropIndex("dbo.Reviews", new[] { "EmployeeId" });
            DropIndex("dbo.Changes", new[] { "Type_Change_typeId" });
            DropIndex("dbo.Changes", new[] { "EmployeeId" });
            DropIndex("dbo.Views", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AffiliationId" });
            DropTable("dbo.TestModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Sources");
            DropTable("dbo.SourcePositions");
            DropTable("dbo.Publications");
            DropTable("dbo.Publication_category");
            DropTable("dbo.Files");
            DropTable("dbo.Reviews");
            DropTable("dbo.Editors");
            DropTable("dbo.Comments");
            DropTable("dbo.Change_type");
            DropTable("dbo.Employees");
            DropTable("dbo.Changes");
            DropTable("dbo.Author_list");
            DropTable("dbo.Views");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Affiliations");
            DropTable("dbo.Admins");
        }
    }
}
