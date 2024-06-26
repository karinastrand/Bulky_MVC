﻿using Microsoft.EntityFrameworkCore;
using BulkyBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace BulkyBook.DataAccess.Data;

public class ApplicationDbContext :IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
            
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }   
    public DbSet<Company> Companies { get; set; }
    public DbSet<ShoppingCart> ProductCarts { get; set; }
    public DbSet<ApplicationUser> applicationUsers { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       modelBuilder.Entity<Category>().HasData(
                new Category
                { 
                    CategoryId = 1, 
                    Name = "Action", 
                    DisplayOrder = 1 
                },
                 new Category
                 {
                     CategoryId = 2,
                     Name = "SciFi",
                     DisplayOrder = 2
                 },
                 new Category
                 {
                     CategoryId = 3,
                     Name = "History",
                     DisplayOrder = 3
                 }
           );
        modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id=1,
                    Name="Tech Solution",
                    StreetAdress="123 Tech St",
                    City="Tech City",
                    PostalCode="12121",
                    State="IL",
                    Phone="12321122"
                },
                new Company
                {
                    Id = 2,
                    Name = "Vivid Books",
                    StreetAdress = "999 Vid St",
                    City = "Vide City",
                    PostalCode = "13421",
                    State = "IL",
                    Phone = "54321122"
                },
                new Company
                {
                    Id = 3,
                    Name = "Readers Club",
                    StreetAdress = "199 Main St",
                    City = "Lala Land",
                    PostalCode = "12121",
                    State = "NY",
                    Phone = "54344122"
                }


           );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = 1,
                Title = "Fortune of Time",
                Author = "Billy Spark",
                Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "SWD9999001",
                ListPrice = 99,
                Price = 90,
                Price50 = 85,
                Price100 = 80,
                CategoryId = 2,
            


            },
                new Product
                {
                    ProductId = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 3,
                  

                },
                new Product
                {
                    ProductId = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId= 1,
                   


                },
                new Product
                {
                    ProductId = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId=1,
                    


                },
                new Product
                {
                    ProductId = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId=2,
                    
                    
                },
                new Product
                {
                    ProductId = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId=3,
                   

                }
            );

    }

}
