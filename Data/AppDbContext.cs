using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Order.Models;
namespace Order.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public DbSet<CustomerModel> Customer { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ordermodel> Order { get; set; }
        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Ordermodel>().HasData(new Ordermodel
        //    {
        //        OrdermodelId = 1,
        //        CustomerId = 1,
        //        OrderDate = new DateTime(2024, 1, 1), 
        //        Status = "Pending",
        //        Total = 0
        //    });


        //    modelBuilder.Entity<OrderItem>().HasData(new OrderItem { OrderItemId = 1001, OrderId = 1, Id = 1001, Price = 22.5f, Quantity = 10 });
        //    modelBuilder.Entity<OrderItem>().HasData(new OrderItem { OrderItemId = 1002, OrderId = 1, Id = 1002, Price = 50.4f, Quantity = 20 });
        //    modelBuilder.Entity<OrderItem>().HasData(new OrderItem { OrderItemId = 1003, OrderId = 1, Id = 1003, Price = 45.6f, Quantity = 30 });
        //    modelBuilder.Entity<OrderItem>().HasData(new OrderItem { OrderItemId = 1004, OrderId = 1, Id = 1004, Price = 34.7f, Quantity = 50 });


        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<CustomerModel>().HasData(new CustomerModel
            {
                Id = 1,
                Name = "John Doe",
                ContactInfo = "johndoe@example.com"
            });

            modelBuilder.Entity<Ordermodel>().HasData(new Ordermodel
            {
                OrderId = 1001,
                CustomerId = 1,
                OrderDate = new DateTime(2024, 1, 1),
                Status = "Pinding",
                Total = 0,
               
            });

            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Description= "Device" ,Name = "Laptop", Price = 3500 , Quantity = 100 },
                new Product { Id = 2, Description = "Device", Name = "Smartphone", Price = 1500 , Quantity = 100 },
                new Product { Id = 3, Description = "Device", Name = "Keyboard", Price = 100 , Quantity = 100 },
                new Product { Id = 4, Description = "Device", Name = "Mouse", Price = 75 , Quantity = 100 });

            modelBuilder.Entity<OrderItem>().HasData(new OrderItem { OrderItemId = 1004, OrderId = 1001, ProductId = 4 ,Quantity = 3});

        }


    }
}

