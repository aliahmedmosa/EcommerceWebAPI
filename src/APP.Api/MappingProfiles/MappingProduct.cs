using APP.Api.Helper;
using APP.Core.Dtos;
using APP.Core.Entities;
using AutoMapper;

namespace APP.Api.MappingProfiles
{
    public class MappingProduct:Profile
    {
        public MappingProduct()
        {
            //CreateMap<source,distenation>().ReverseMap();
            //CreateMap<distenation,source>().ReverseMap();
            //Does not matter where is the source or the distenation ReverseMap() will solve this issue .....
            CreateMap<Product, ProductDto>()
                .ForMember(d=>d.CategoryName,o=>o.MapFrom(s=>s.Category.Name))      //to get category with products 
                .ForMember(d=>d.ProductPicture,o=>o.MapFrom<ProductUrlResolver>())   //to get Product Picture
                .ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            
        }
    }
}
