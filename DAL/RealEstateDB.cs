namespace DAL
{
    using DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class RealEstateDB : DbContext
    {
        public RealEstateDB()
            : base("name=RealEstateDB")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }

        public DbSet<DB_RealEstate> RealEstates { get; set; }
        public DbSet<DB_User> Users { get; set; }
        public DbSet<DB_Category> Categories { get; set; }
        public DbSet<DB_Subcategory> Subcategories { get; set; }
    }
    
}