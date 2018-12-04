using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DAL;
using DAL.Entities;
using Ninject;
using NinjectConfiguration;

namespace BLL
{
    public class Category_Operations : ICategory_Operations
    {
        IKernel ninjectKernel;

        public IUnitOfWork uow { get; set; }
        public Category_Operations()
        {
            ninjectKernel = new StandardKernel(new BLL_NinjectConfig());
            this.uow = ninjectKernel.Get<IUnitOfWork>();
        }

        public Category_Operations(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            IEnumerable<DB_Category> dbcategories = uow.Categories.Get();
            categories = Mapper.Map<IEnumerable<DB_Category>, List<Category>>(dbcategories);
            return categories;
        }
        public void SaveCategory(string name)
        {
            DB_Category categ = new DB_Category
            {
                Name = name
            };
            uow.Categories.Create(categ);
            uow.Save();
        }

        public void deleteCategory(int CategoryId)
        {
            DB_Category category = uow.Categories.FindById(CategoryId);
            if (category != null)
                uow.Categories.Remove(category);
            uow.Save();
        }
    }
}
