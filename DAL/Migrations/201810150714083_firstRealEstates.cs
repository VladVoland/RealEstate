namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstRealEstates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DB_Lot", "Category_CategoryId", "dbo.DB_Category");
            DropForeignKey("dbo.DB_Lot", "Owner_UserId", "dbo.DB_User");
            DropForeignKey("dbo.DB_Lot", "Subcategory_SubcategoryId", "dbo.DB_Subcategory");
            DropIndex("dbo.DB_Lot", new[] { "Category_CategoryId" });
            DropIndex("dbo.DB_Lot", new[] { "Owner_UserId" });
            DropIndex("dbo.DB_Lot", new[] { "Subcategory_SubcategoryId" });
            CreateTable(
                "dbo.DB_RealEstate",
                c => new
                    {
                        RealEstateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Specification = c.String(nullable: false, maxLength: 1000),
                        Location = c.String(nullable: false, maxLength: 150),
                        Price = c.Int(nullable: false),
                        SubcategoryId = c.Int(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Category_CategoryId = c.Int(nullable: false),
                        Owner_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RealEstateId)
                .ForeignKey("dbo.DB_Category", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.DB_User", t => t.Owner_UserId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Owner_UserId);
            
            AddColumn("dbo.DB_User", "Surname", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.DB_User", "Syrname");
            DropColumn("dbo.DB_User", "Passport");
            DropTable("dbo.DB_Lot");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DB_Lot",
                c => new
                    {
                        LotId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Specification = c.String(nullable: false, maxLength: 1000),
                        Bet = c.Int(nullable: false),
                        Step = c.Int(),
                        Duration = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Winner = c.String(maxLength: 200),
                        Category_CategoryId = c.Int(nullable: false),
                        Owner_UserId = c.Int(nullable: false),
                        Subcategory_SubcategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LotId);
            
            AddColumn("dbo.DB_User", "Passport", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.DB_User", "Syrname", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.DB_RealEstate", "Owner_UserId", "dbo.DB_User");
            DropForeignKey("dbo.DB_RealEstate", "Category_CategoryId", "dbo.DB_Category");
            DropIndex("dbo.DB_RealEstate", new[] { "Owner_UserId" });
            DropIndex("dbo.DB_RealEstate", new[] { "Category_CategoryId" });
            DropColumn("dbo.DB_User", "Surname");
            DropTable("dbo.DB_RealEstate");
            CreateIndex("dbo.DB_Lot", "Subcategory_SubcategoryId");
            CreateIndex("dbo.DB_Lot", "Owner_UserId");
            CreateIndex("dbo.DB_Lot", "Category_CategoryId");
            AddForeignKey("dbo.DB_Lot", "Subcategory_SubcategoryId", "dbo.DB_Subcategory", "SubcategoryId");
            AddForeignKey("dbo.DB_Lot", "Owner_UserId", "dbo.DB_User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.DB_Lot", "Category_CategoryId", "dbo.DB_Category", "CategoryId", cascadeDelete: true);
        }
    }
}
