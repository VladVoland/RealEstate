using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using AutoMapper;
using BLL;
using Ninject;
using NinjectConfiguration;

namespace OnlineAuction.Controllers
{
    public class SubcategoryController : ApiController
    {
        IKernel ninjectKernel;
        ISubcategory_Operations SOperations;
        public SubcategoryController()
        {
            ninjectKernel = new StandardKernel(new NinjectConfig());
            SOperations = ninjectKernel.Get<ISubcategory_Operations>();
        }

        [HttpGet]
        public IEnumerable<SubcategoryModel> GetSubcategory()
        {
            IEnumerable<Subcategory> tempSubcategories = SOperations.GetSubcategories();
            IEnumerable<SubcategoryModel> subcategories;
            subcategories = Mapper.Map<IEnumerable<Subcategory>, IEnumerable<SubcategoryModel>>(tempSubcategories);
            return subcategories;
        }
        [HttpGet]
        [Route("api/subcategory/get/{categoryName}")]
        public IEnumerable<SubcategoryModel> GetSubcategoryByCateg(string categoryName)
        {
            IEnumerable<Subcategory> tempSubcategories = SOperations.GetSubcategoriesByCateg(categoryName);
            IEnumerable<SubcategoryModel> subcategories;
            subcategories = Mapper.Map<IEnumerable<Subcategory>, IEnumerable<SubcategoryModel>>(tempSubcategories);
            return subcategories;
        }

        [HttpPost]
        [Route("api/saveSubcategory/{Categoryname}/{Subcategoryname}")]
        public IHttpActionResult PostSubcategory(string Categoryname, string Subcategoryname)
        {
            string patt = @"^[\d|\D]{1,50}$";
            if (string.IsNullOrWhiteSpace(Subcategoryname) || string.IsNullOrWhiteSpace(Categoryname))
                return BadRequest("Please, enter subcategory name");
            else if (!Regex.IsMatch(Subcategoryname, patt))
                return BadRequest("Subcategory name is too longs");
            else if (!Regex.IsMatch(Categoryname, patt))
                return BadRequest("Category name name is too longs");
            else
            {
                SOperations.SaveSubcategory(Subcategoryname, Categoryname);
                return Ok();
            }
        }

        [HttpDelete]
        [Route("api/deleteSubcategory/{id}")]
        public void Delete(int id)
        {
            SOperations.deleteSubcategory(id);
        }
    }
}
