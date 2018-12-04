using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DAL;
using DAL.Entities;

namespace BLL
{
    public static class BLL_AutoMapper
    {
        public static IMapperConfigurationExpression getConfiguration(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<DB_RealEstate, RealEstate>()
                    .ForMember("Category", x => x.MapFrom(c => c.Category.Name))
                    .ForMember("OwnerInfo", x => x.MapFrom(c => c.Owner.Surname + " " + c.Owner.Name + " " + c.Owner.Patronymic + " Phone number: +380" + c.Owner.PhoneNumber))
                    .ForMember("Owner", x => x.MapFrom(c => c.Owner.UserId)); 
            cfg.CreateMap<DB_User, User>();
            cfg.CreateMap<DB_Category, Category>();
            cfg.CreateMap<DB_Subcategory, Subcategory>().ForMember("Category", x => x.MapFrom(c => c.Category.Name));
            cfg.CreateMap<RealEstate, DB_RealEstate>()
                .ForMember("Category", x => x.Ignore())
                .ForMember("Owner", x => x.Ignore());
            return cfg;
        }

        public static void Initialize(){
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DB_RealEstate, RealEstate>()
                    .ForMember("Category", x => x.MapFrom(c => c.Category.Name))
                    .ForMember("OwnerInfo", x => x.MapFrom(c => c.Owner.Surname + " " + c.Owner.Name + " " + c.Owner.Patronymic + " Phone number: +380" + c.Owner.PhoneNumber))
                    .ForMember("Owner", x => x.MapFrom(c => c.Owner.UserId));
                cfg.CreateMap<DB_User, User>();
                cfg.CreateMap<DB_Category, Category>();
                cfg.CreateMap<DB_Subcategory, Subcategory>().ForMember("Category", x => x.MapFrom(c => c.Category.Name));
                cfg.CreateMap<RealEstate, DB_RealEstate>()
                    .ForMember("Category", x => x.Ignore())
                    .ForMember("Owner", x => x.Ignore());
            });
        }
    }
}
