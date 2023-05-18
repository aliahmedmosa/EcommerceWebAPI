using APP.Api.Dtos;
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
            CreateMap<ProductDto, Product>().ReverseMap();
            
        }
    }
}
