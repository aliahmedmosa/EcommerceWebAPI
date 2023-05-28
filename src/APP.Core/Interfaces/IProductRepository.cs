using APP.Core.Dtos;
using APP.Core.Entities;
using APP.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //Here you will write special implementation for Product for future
        Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(UpdateProductDto dto);
    }
}
