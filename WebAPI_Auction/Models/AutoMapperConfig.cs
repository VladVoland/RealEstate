using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace BLL
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg = BLL_AutoMapper.getConfiguration(cfg);
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<Subcategory, SubcategoryModel>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<RealEstate, RealEstateModel>();
            });
        }
    }
}
