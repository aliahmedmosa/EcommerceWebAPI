using APP.Core.Dtos;
using APP.Core.Entities;
using APP.Core.Interfaces;
using APP.Infrastructure.Data;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        // Configure IFileProvider in ApiRegestration.cs in Extensions and configue it in program.cs


        private readonly ApplicationDbContext context;
        private readonly IFileProvider fileProvider;
        private readonly IMapper mapper;
        

        public ProductRepository(ApplicationDbContext context,IFileProvider fileProvider,IMapper mapper) : base(context)
        {
            this.context = context;
            this.fileProvider = fileProvider;
            this.mapper = mapper;
           
        }


        // overload add async to upload image 
        public async Task<bool> AddAsync(CreateProductDto dto)
        {
            var src = "";
            if (dto.Image is not null)
            {
                /*
                * To upload Image
                */

                //Start Implementation 
                var root = "/images/products/";
                var productImageName= $"{Guid.NewGuid()}"+dto.Image.FileName;
                if (!Directory.Exists("wwwroot" + root))
                {
                    Directory.CreateDirectory("wwwroot"+root);
                }
                src = root + productImageName;
                var picInfo = fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;
                using (var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                   await dto.Image.CopyToAsync(fileStream);
                }

                //End Implementation 

            }
            //Create new product with uploaded Image ----------
            var response = mapper.Map<Product>(dto);
            response.ProductPicture = src;
            await context.Products.AddAsync(response);
            await context.SaveChangesAsync();

            return true;
        }



        public async Task<bool> UpdateAsync(UpdateProductDto dto)
        {
            var currentProduct = await context.Products.FindAsync(dto.Id);
            if (currentProduct != null) 
            {
                var src = "";
                if (dto.Image is not null)
                {
                    /*
                    * To upload Image
                    */

                    //Start Implementation 
                    var root = "/images/products/";
                    var productImageName = $"{Guid.NewGuid()}" + dto.Image.FileName;
                    if (!Directory.Exists("wwwroot" + root))
                    {
                        Directory.CreateDirectory("wwwroot" + root);
                    }
                    src = root + productImageName;
                    var picInfo = fileProvider.GetFileInfo(src);
                    var rootPath = picInfo.PhysicalPath;
                    using (var fileStream = new FileStream(rootPath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(fileStream);
                    }

                    //End Implementation 
                }


                //Remove old Picture
                if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                {
                    var picInfo = fileProvider.GetFileInfo(currentProduct.ProductPicture);
                    var rootPath = picInfo.PhysicalPath;
                    System.IO.File.Delete(rootPath);
                }


                //update Product
                var response = mapper.Map<Product>(dto);
                response.ProductPicture = src;
                context.Products.Update(response);
                await context.SaveChangesAsync();

                
                return true;
            }
            return false;
        }
    }
}
