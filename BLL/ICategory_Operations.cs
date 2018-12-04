using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICategory_Operations
    {
        IUnitOfWork uow { get; set; }
        List<Category> GetCategories();
        void SaveCategory(string name);
        void deleteCategory(int categoryId);
    }
}
