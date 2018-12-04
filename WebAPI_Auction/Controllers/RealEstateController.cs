using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http;
using AutoMapper;
using BLL;
using Ninject;
using NinjectConfiguration;

namespace OnlineAuction.Controllers
{
    public class RealEstateController : ApiController
    {
        IKernel ninjectKernel;
        IRealEstate_Operations REOperations;
        public RealEstateController()
        {
            ninjectKernel = new StandardKernel(new NinjectConfig());
            REOperations = ninjectKernel.Get<IRealEstate_Operations>();
        }

        [HttpGet]
        [Route("api/realEstate/GetRealEstatesBySearch")]
        public IEnumerable<RealEstateModel> GetRealEstatesBySearch(string category, string subcategory, string keyword)
        {
            if (category == null) category = "";
            if (subcategory == null) subcategory = "";
            if (keyword == null) keyword = "";
            if (!string.IsNullOrWhiteSpace(category) || !string.IsNullOrWhiteSpace(subcategory) || !string.IsNullOrWhiteSpace(keyword))
            {
                IEnumerable<RealEstate> tempRealEstates = REOperations.GetBySearch(category, subcategory, keyword);
                IEnumerable<RealEstateModel> realEstates = Mapper.Map<IEnumerable<RealEstate>, IEnumerable<RealEstateModel>>(tempRealEstates);
                return realEstates;
            }
            IEnumerable<RealEstate> tempCRealEstates = REOperations.GetСonfirmedRealEstates();
            IEnumerable<RealEstateModel> cRealEstates = Mapper.Map<IEnumerable<RealEstate>, IEnumerable<RealEstateModel>>(tempCRealEstates);
            return cRealEstates;
        }

        [HttpGet]
        [Route("api/realEstate/GetUnconfirmedRealEstates")]
        public IEnumerable<RealEstateModel> GetUnconfirmedRealEstates()
        {
            IEnumerable<RealEstate> tempRealEstates = REOperations.GetUnconfirmedRealEstates();
            IEnumerable<RealEstateModel> realEstates = Mapper.Map<IEnumerable<RealEstate>, IEnumerable<RealEstateModel>>(tempRealEstates);
            return realEstates;
        }
        [HttpGet]
        [Route("api/realEstate/GetConfirmedRealEstates")]
        public IEnumerable<RealEstateModel> GetConfirmedRealEstates()
        {
            IEnumerable<RealEstate> tempRealEstates = REOperations.GetСonfirmedRealEstates();
            IEnumerable<RealEstateModel> realEstates = Mapper.Map<IEnumerable<RealEstate>, IEnumerable<RealEstateModel>>(tempRealEstates);
            return realEstates;
        }

        [HttpPost]
        [Route("api/realEstate/newRealEstate")]
        public IHttpActionResult PostRealEstate(RealEstateModel _realEstate)
        {
            if (string.IsNullOrWhiteSpace(_realEstate.Name) || string.IsNullOrWhiteSpace(_realEstate.Specification)
                || string.IsNullOrWhiteSpace(_realEstate.Category) || _realEstate.Price == 0)
            {
                return BadRequest("Please, correct your inputs");
            }
            else if (!Regex.IsMatch(_realEstate.Name, @"^[\d|\D]{1,50}$"))
                return BadRequest("Name is too longs");
            else if (!Regex.IsMatch(_realEstate.Specification, @"^[\d|\D]{1,1000}$"))
                return BadRequest("Specification is too longs");
            else
            {
                RealEstate realEstate = Mapper.Map<RealEstateModel, RealEstate>(_realEstate);
                REOperations.SaveRealEstate(realEstate);
                return Ok();
            }
        }

        [HttpPut]
        [Route("api/realEstate/confirm/{realEstateId}")]
        public void Confirm(int realEstateId)
        {
            REOperations.Confirm(realEstateId);
        }

        [HttpPut]
        [Route("api/realEstate/change/{name}/{specification}/{location}/{realEstateId}")]
        public IHttpActionResult Change(string name, string specification, string location, int realEstateId)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(specification) || string.IsNullOrWhiteSpace(location))
                return BadRequest("Please, correct your inputs");
            else if (!Regex.IsMatch(name, @"^[\d|\D]{1,50}$)"))
                return BadRequest("Name is too longs");
            else if (!Regex.IsMatch(specification, @"^[\d|\D]{1,1000}$)"))
                return BadRequest("Specification is too longs");
            else if (!Regex.IsMatch(location, @"^[\d|\D]{1,150}$)"))
                return BadRequest("Location is too longs");
            else
            {
                bool result = REOperations.Change(name, specification, location, realEstateId);
                if (!result)
                    return BadRequest("Please, input correct information");
                else
                    return Ok();
            }
        }

        [HttpDelete]
        [Route("api/realEstate/detete/{id}")]
        public void Delete(int id)
        {
            REOperations.deleteRealEstate(id);
        }
    }
}
