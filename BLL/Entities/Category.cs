using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Category
    {
        public Category() { }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Subcategory> Subcategories { get; set; }
        public List<RealEstate> RealEstates { get; set; }
    }
}
