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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        
        private readonly IFileProvider fileProvider;
        private readonly IMapper mapper;

        public ICategoryRepository CategoryRepository { get; } 

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(ApplicationDbContext context,IFileProvider fileProvider,IMapper mapper)
        {
            this.context = context;
            
            this.fileProvider = fileProvider;
            this.mapper = mapper;
            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context,fileProvider,mapper);
        }
    }
}
