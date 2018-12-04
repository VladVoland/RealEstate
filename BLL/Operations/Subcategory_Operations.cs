using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ninject;
using NinjectConfiguration;

namespace BLL
{
    public class Subcategory_Operations : ISubcategory_Operations
    {
        IKernel ninjectKernel;
        public IUnitOfWork uow { get; set; }
        public Subcategory_Operations(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public Subcategory_Operations()
        {
            ninjectKernel = new StandardKernel(new BLL_NinjectConfig());
            this.uow = ninjectKernel.Get<IUnitOfWork>();
        }
        public List<Subcategory> GetSubcategories()
        {
            List<Subcategory> subcategories = new List<Subcategory>();
            IEnumerable<DB_Subcategory> dbsubcategories = uow.Subcategories.GetWithInclude(s => s.Category);
            subcategories = Mapper.Map<IEnumerable<DB_Subcategory>, List<Subcategory>>(dbsubcategories);
            return subcategories;
        }

        public List<Subcategory> GetSubcategoriesByCateg(string categoryName)
        {
            List<Subcategory> subcategories = new List<Subcategory>();
            IEnumerable<DB_Subcategory> dbsubcategories = uow.Subcategories.GetWithInclude(s => s.Category);
            foreach (DB_Subcategory subc in dbsubcategories)
            {
                if (subc.Category.Name == categoryName)
                    subcategories.Add(Mapper.Map<DB_Subcategory, Subcategory>(subc));
            }
            return subcategories;
        }

        public void SaveSubcategory(string SubcategoryName, string CategoryName)
        {
            int categ = 0;
            IEnumerable<DB_Category> categories = uow.Categories.Get();
            foreach (DB_Category c in categories)
            {
                if (c.Name == CategoryName) categ = c.CategoryId;
            }
            DB_Subcategory subcateg = new DB_Subcategory { Name = SubcategoryName, Category = uow.Categories.FindById(categ)};
            uow.Subcategories.Create(subcateg);
            uow.Save();
        }

        public void deleteSubcategory(int SubcategoryId)
        {
            DB_Subcategory subcategory = uow.Subcategories.FindById(SubcategoryId);
            if (subcategory != null)
                uow.Subcategories.Remove(subcategory);
            uow.Save();
        }
    }
}
