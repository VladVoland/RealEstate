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
    public class RealEstate_Operations : IRealEstate_Operations
    {
        IKernel ninjectKernel;
        public IUnitOfWork uow { get; set; }
        public RealEstate_Operations(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public RealEstate_Operations()
        {
            ninjectKernel = new StandardKernel(new BLL_NinjectConfig());
            this.uow = ninjectKernel.Get<IUnitOfWork>();
        }
        public void SaveRealEstate(RealEstate _realEstate)
        {
            DB_RealEstate realEstate = Mapper.Map<RealEstate, DB_RealEstate>(_realEstate);

            IEnumerable<DB_Category> categories = uow.Categories.Get();
            foreach (DB_Category c in categories)
            {
                if (c.Name == _realEstate.Category)
                {
                    realEstate.Category = uow.Categories.FindById(c.CategoryId);
                }
            }
            if (!string.IsNullOrWhiteSpace(_realEstate.Subcategory))
            {
                IEnumerable<DB_Subcategory> subcategories = uow.Subcategories.Get();
                foreach (DB_Subcategory sc in subcategories)
                {
                    if (sc.Name == _realEstate.Subcategory)
                        realEstate.SubcategoryId = sc.SubcategoryId;
                }
            }
            else
                realEstate.SubcategoryId = null;
            DB_User owner = uow.Users.FindById(_realEstate.Owner);
            realEstate.Owner = owner;

            uow.RealEstates.Create(realEstate);
            uow.Save();
        }

        public void deleteRealEstate(int realEstateId)
        {
            DB_RealEstate realEstate = uow.RealEstates.FindById(realEstateId);
            if (realEstate != null)
            {
                uow.RealEstates.Remove(realEstate);
                uow.Save();
            }
        }

        public List<RealEstate> GetUnconfirmedRealEstates()
        {
            List<RealEstate> realEstates = new List<RealEstate>();
            IEnumerable<DB_RealEstate> dbRealEstates = uow.RealEstates.GetWithInclude(rs => (rs.Category), rs => (rs.Owner));
            foreach (DB_RealEstate realEstate in dbRealEstates)
            {
                if (realEstate.StartDate == new DateTime())
                {
                    RealEstate tempL = Mapper.Map<DB_RealEstate, RealEstate>(realEstate);
                    realEstates.Add(tempL);
                }
            }
            foreach(RealEstate realEstate in realEstates)
            {
                if (realEstate.SubcategoryId > 0)
                {
                    DB_Subcategory dbsubc = uow.Subcategories.FindById(realEstate.SubcategoryId);
                    realEstate.Subcategory = dbsubc.Name;
                }
                else realEstate.Subcategory = "";
            }
            return realEstates;
        }
        public List<RealEstate> GetСonfirmedRealEstates()
        {
            List<RealEstate> realEstates = new List<RealEstate>();
            IEnumerable<DB_RealEstate> dbrealEstates = uow.RealEstates.GetWithInclude(l => (l.Category), l => (l.Owner));
            foreach (DB_RealEstate realEstate in dbrealEstates)
            {
                if (realEstate.StartDate != new DateTime(0001, 1, 1, 0, 0, 0))
                {
                    RealEstate tempL = Mapper.Map<DB_RealEstate, RealEstate>(realEstate);
                    realEstates.Add(tempL);
                }
            }
            foreach (RealEstate realEstate in realEstates)
            {
                if (realEstate.SubcategoryId > 0)
                {
                    DB_Subcategory dbsubc = uow.Subcategories.FindById(realEstate.SubcategoryId);
                    realEstate.Subcategory = dbsubc.Name;
                }
                else realEstate.Subcategory = "";
            }
            return realEstates;
        }

        public bool Change(string name, string specification, string location, int realEstateId)
        {
            DB_RealEstate realEstate = uow.RealEstates.FindById(realEstateId);
            realEstate.Name = name;
            realEstate.Specification = specification;
            realEstate.Location = location;
            uow.RealEstates.Update(realEstate);
            uow.Save();

            return true;
        }

        public List<RealEstate> GetBySearch(string _category, string _subcategory, string keyword)
        {
            if (_category == null) _category = "";
            if (_subcategory == null) _subcategory = "";
            if (keyword == null) keyword = "";
            List<RealEstate> confirmedrealEstates = GetСonfirmedRealEstates();
            List<RealEstate> realEstates = new List<RealEstate>();
            foreach (RealEstate realEstate in confirmedrealEstates)
            {
                if(_category != "" && realEstate.Category.Contains(_category))
                    realEstates.Add(realEstate);
                else if (_subcategory != "" && realEstate.Subcategory.Contains(_subcategory))
                    realEstates.Add(realEstate);
                else if(keyword != "")
                {
                    if (realEstate.Name.Contains(keyword))
                        realEstates.Add(realEstate);
                    else if (realEstate.Specification.Contains(keyword))
                        realEstates.Add(realEstate);
                }
            }

            return realEstates;
        }

        public void Confirm(int realEstateId)
        {
            UnitOfWork uow = new UnitOfWork();
            DB_RealEstate realEstate = uow.RealEstates.FindById(realEstateId);
            DateTime tempDT = DateTime.Now;
            realEstate.StartDate = tempDT;
            uow.RealEstates.Update(realEstate);
            uow.Save();
        }
    }
}
