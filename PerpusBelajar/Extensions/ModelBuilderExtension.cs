using Microsoft.EntityFrameworkCore;
using PerpusBelajar.Constant;
using PerpusBelajar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerpusBelajar.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Add To Table here to define table name



            //To Table to define table name Book and Seeding Book Table
            modelBuilder.Entity<Book>().ToTable("Book").HasData(
                new Book
                {
                    Id = 1,
                    Title = "Harry Potter and The Sorcerer's Stone",
                    Author = "JK Rowling",
                    ISBN = "000000000001",
                    Quantity = 1,
                    Category = BookCategory.Novel,
                    ImageFileName = "Book1.jpg"
                });


        }
    }
}
