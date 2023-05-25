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
                .ForMember(d=>d.CategoryName,o=>o.MapFrom(s=>s.Category.Name))       //To include category name with products 
                .ForMember(d=>d.ProductPicture,o=>o.MapFrom<ProductUrlResolver>())   //To include Full product Picture source to appeare 
                .ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            
        }
    }
}
