using APP.Core.Dtos;
using APP.Core.Entities;
using AutoMapper;

namespace APP.Api.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /*
         * Here We make this class to get Product picture 
         * 1- App URL from appsettings "ApiURL"
         * 2- Picture name from source.ProductPicture
         * 3- Call this calss in MappingProduct class will return  configuration["ApiURL"]+source.ProductPicture if picture is found
        */
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductPicture))
            {
                return configuration["ApiURL"]+source.ProductPicture;
            }
            return null;
        }
    }
}
