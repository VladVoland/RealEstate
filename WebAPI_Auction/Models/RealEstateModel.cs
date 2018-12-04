using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RealEstateModel
    {
        public RealEstateModel() { }
        public int RealEstateId { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public int SubcategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public int Owner { get; set; }
        public string OwnerInfo { get; set; }
    }
}
