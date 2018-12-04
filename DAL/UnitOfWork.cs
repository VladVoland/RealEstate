using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private RealEstateDB db = new RealEstateDB();
        private dbContextRepository<DB_RealEstate> RealEstateRepository;
        private dbContextRepository<DB_User> UserRepository;
        private dbContextRepository<DB_Category> CategoryRepository;
        private dbContextRepository<DB_Subcategory> SubcategoryRepository;

        public dbContextRepository<DB_RealEstate> RealEstates
        {
            get
            {
                if (RealEstateRepository == null)
                    RealEstateRepository = new dbContextRepository<DB_RealEstate>(db);
                return RealEstateRepository;
            }
        }

        public dbContextRepository<DB_User> Users
        {
            get
            {
                if (UserRepository == null)
                    UserRepository = new dbContextRepository<DB_User>(db);
                return UserRepository;
            }
        }

        public dbContextRepository<DB_Category> Categories
        {
            get
            {
                if (CategoryRepository == null)
                    CategoryRepository = new dbContextRepository<DB_Category>(db);
                return CategoryRepository;
            }
        }

        public dbContextRepository<DB_Subcategory> Subcategories
        {
            get
            {
                if (SubcategoryRepository == null)
                    SubcategoryRepository = new dbContextRepository<DB_Subcategory>(db);
                return SubcategoryRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
