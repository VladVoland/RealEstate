using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        dbContextRepository<DB_RealEstate> RealEstates { get; }
        dbContextRepository<DB_User> Users { get; }
        dbContextRepository<DB_Category> Categories { get; }
        dbContextRepository<DB_Subcategory> Subcategories { get; }

        void Save();
        void Dispose();
    }
}
