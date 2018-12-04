using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IRealEstate_Operations
    {
        IUnitOfWork uow { get; set; }
        void SaveRealEstate(RealEstate _realEstate);
        void deleteRealEstate(int realEstateId);
        List<RealEstate> GetUnconfirmedRealEstates();
        List<RealEstate> GetСonfirmedRealEstates();
        bool Change(string name, string specification, string location, int realEstated);
        List<RealEstate> GetBySearch(string _category, string _subcategory, string keyword);
        void Confirm(int realEstateId);
    }
}
