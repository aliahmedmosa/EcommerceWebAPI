using APP.Api.Dtos;
using APP.Core.Entities;
using AutoMapper;

namespace APP.Api.MappingProfiles
{
    public class MappingCategory:Profile
    {
        //Configuration for auto mapper .................
        public MappingCategory()
        {

            //CreateMap<source,distenation>().ReverseMap();
            //CreateMap<distenation,source>().ReverseMap();
            //Does not matter where is the source or the distenation ReverseMap() will solve this issue .....
            CreateMap<CategoryDto,Category>().ReverseMap();
            CreateMap<ListingCategoryDto,Category>().ReverseMap();
        }
    }
}
