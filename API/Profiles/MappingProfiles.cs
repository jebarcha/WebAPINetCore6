using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using API.Dtos;

namespace API.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
            .ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Brand, BrandDto>()
                .ReverseMap();

            CreateMap<Product, ProductListDto>()
                .ForMember(dest => dest.Brand, origin => origin.MapFrom(origin => origin.Brand.Name))
                .ForMember(dest => dest.Category, origin => origin.MapFrom(origin => origin.Category.Name))
                .ReverseMap()
                .ForMember(origin => origin.Category, dest => dest.Ignore())
                .ForMember(origin => origin.Brand, dest => dest.Ignore());

            CreateMap<Product, ProductAddUpdateDto>()
                .ReverseMap()
                .ForMember(origin => origin.Category, dest => dest.Ignore())
                .ForMember(origin => origin.Brand, dest => dest.Ignore());
        }
    }
}
