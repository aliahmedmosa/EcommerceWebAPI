﻿using APP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");


            //Seed data
            builder.HasData(
               new Product
               {
                   Id = 1,
                   Name = "Product One",
                   Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley",
                   Price = 3000,
                   ProductPicture="https://",
                   CategoryId = 1
               },
               new Product
               {
                   Id = 2,
                   Name = "Product Two",
                   Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley",
                   Price = 2500,
                   ProductPicture = "https://",
                   CategoryId = 2
               },
               new Product
               {
                   Id = 3,
                   Name = "Product Three",
                   Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley",
                   Price = 2000,
                   ProductPicture = "https://",
                   CategoryId = 3
               }
               );
        }
    }
}
