namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DB_Lot", "DB_User_UserId", c => c.Int());
            AddColumn("dbo.DB_Lot", "DB_User_UserId1", c => c.Int());
            AddColumn("dbo.DB_User", "DB_Lot_LotId", c => c.Int());
            CreateIndex("dbo.DB_Lot", "DB_User_UserId");
            CreateIndex("dbo.DB_Lot", "DB_User_UserId1");
            CreateIndex("dbo.DB_User", "DB_Lot_LotId");
            AddForeignKey("dbo.DB_Lot", "DB_User_UserId", "dbo.DB_User", "UserId");
            AddForeignKey("dbo.DB_Lot", "DB_User_UserId1", "dbo.DB_User", "UserId");
            AddForeignKey("dbo.DB_User", "DB_Lot_LotId", "dbo.DB_Lot", "LotId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DB_User", "DB_Lot_LotId", "dbo.DB_Lot");
            DropForeignKey("dbo.DB_Lot", "DB_User_UserId1", "dbo.DB_User");
            DropForeignKey("dbo.DB_Lot", "DB_User_UserId", "dbo.DB_User");
            DropIndex("dbo.DB_User", new[] { "DB_Lot_LotId" });
            DropIndex("dbo.DB_Lot", new[] { "DB_User_UserId1" });
            DropIndex("dbo.DB_Lot", new[] { "DB_User_UserId" });
            DropColumn("dbo.DB_User", "DB_Lot_LotId");
            DropColumn("dbo.DB_Lot", "DB_User_UserId1");
            DropColumn("dbo.DB_Lot", "DB_User_UserId");
        }
    }
}
