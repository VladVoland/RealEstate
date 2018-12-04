using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class DB_RealEstate
    {
        [Key]
        [Required]
        public int RealEstateId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Specification { get; set; }
        [Required]
        [MaxLength(150)]
        public string Location { get; set; }

        [Required]
        public int Price { get; set; }

        
        [Required]
        public DB_Category Category { get; set; }
        public int? SubcategoryId { get; set; }
        public DateTime StartDate { get; set; }
        
        [Required]
        public DB_User Owner { get; set; }
    }
}


