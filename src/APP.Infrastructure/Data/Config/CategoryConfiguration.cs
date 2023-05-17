using APP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30);


            //Seed data
            builder.HasData(
               new Category
               {
                   Id = 1,
                   Name = "Category One",
                   Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
               " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley"
               },
               new Category
               {
                   Id = 2,
                   Name = "Category Two",
                   Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
               " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley"
               },
               new Category
               {
                   Id = 3,
                   Name = "Category Three",
                   Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
               " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley"
               },
               new Category
               {
                   Id = 4,
                   Name = "Category Four",
                   Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
               " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley"
               }
               );
        }
    }
}
