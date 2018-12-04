using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ISubcategory_Operations
    {
        IUnitOfWork uow { get; set; }
        List<Subcategory> GetSubcategories();
        List<Subcategory> GetSubcategoriesByCateg(string categoryName);
        void SaveSubcategory(string SubcategoryName, string CategoryName);
        void deleteSubcategory(int SubcategoryId);
    }
}
