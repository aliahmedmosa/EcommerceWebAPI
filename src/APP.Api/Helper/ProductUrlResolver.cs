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
