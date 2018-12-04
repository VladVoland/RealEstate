using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http;
using AutoMapper;
using BLL;
using Ninject;
using NinjectConfiguration;

namespace OnlineAuction.Controllers
{
    public class CategoryController : ApiController
    {
        IKernel ninjectKernel;
        ICategory_Operations COperations;
        public CategoryController()
        {
            ninjectKernel = new StandardKernel(new NinjectConfig());
            COperations = ninjectKernel.Get<ICategory_Operations>();
        }

        [HttpGet]
        [Route("api/category")]
        public IEnumerable<CategoryModel> GetCategories()
        {
            IEnumerable<Category> tempCategs = COperations.GetCategories();
            IEnumerable<CategoryModel> categories = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(tempCategs);
            return categories;
        }

        [HttpPost]
        [Route("api/saveCategory/{name}")]
        public IHttpActionResult PostCategory(string name)
        {
            string patt = @"^[\d|\D]{1,50}$";
            if (string.IsNullOrWhiteSpace(name) || name == "undefined")
                return BadRequest("Please, enter category name");
            else if (!Regex.IsMatch(name, patt))
                return BadRequest("Category name is too longs");
            else
            {
                COperations.SaveCategory(name);
                return Ok("Success");
            }
        }

        [HttpDelete]
        [Route("api/deleteCaregory/{id}")]
        public void Delete(int id)
        {
            COperations.deleteCategory(id);
        }
    }
}
