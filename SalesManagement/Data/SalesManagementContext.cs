using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Models.Entities;

namespace SalesManagement.Data
{
    public class SalesManagementContext : DbContext
    {
        public SalesManagementContext(DbContextOptions<SalesManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ProductType> ProductType { get; set; } = default!;
        public DbSet<Transaction> Transaction { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<TransactionProduct> TransactionProduct { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Title = "Passive Components" },
                new Category { Id = 2, Title = "Semiconductors" },
                new Category { Id = 3, Title = "Voltage Regulators" },
                new Category { Id = 4, Title = "Microcontrollers" },
                new Category { Id = 5, Title = "Sensors" },
                new Category { Id = 6, Title = "Modules" },
                new Category { Id = 7, Title = "Connectors" },
                new Category { Id = 8, Title = "Power Electronics" },
                new Category { Id = 9, Title = "Displays" },
                new Category { Id = 10, Title = "Memory Chips" }
            );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Title = "1/4W Resistor", CategoryId = 1 },
                new ProductType { Id = 2, Title = "Electrolytic Capacitor", CategoryId = 1 },
                new ProductType { Id = 3, Title = "NPN Transistor", CategoryId = 2 },
                new ProductType { Id = 4, Title = "MOSFET", CategoryId = 2 },
                new ProductType { Id = 5, Title = "7805 Regulator", CategoryId = 3 },
                new ProductType { Id = 6, Title = "ATmega Series", CategoryId = 4 },
                new ProductType { Id = 7, Title = "Temperature Sensor", CategoryId = 5 },
                new ProductType { Id = 8, Title = "Relay Module", CategoryId = 6 },
                new ProductType { Id = 9, Title = "JST Connector", CategoryId = 7 },
                new ProductType { Id = 10, Title = "OLED Display", CategoryId = 9 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Title = "Resistor 10kΩ 1/4W", Price = 200, MinQuantity = 10, IsActive = true, TypeId = 1 },
                new Product { Id = 2, Title = "Resistor 1kΩ 1/4W", Price = 150, MinQuantity = 10, IsActive = true, TypeId = 1 },
                new Product { Id = 3, Title = "Capacitor 100uF 25V", Price = 3000, MinQuantity = 20, IsActive = true, TypeId = 2 },
                new Product { Id = 4, Title = "Capacitor 470uF 16V", Price = 3500, MinQuantity = 20, IsActive = true, TypeId = 2 },
                new Product { Id = 5, Title = "Transistor 2N2222 NPN", Price = 1200, MinQuantity = 10, IsActive = true, TypeId = 3 },
                new Product { Id = 6, Title = "MOSFET IRFZ44N", Price = 9000, MinQuantity = 5, IsActive = true, TypeId = 4 },
                new Product { Id = 7, Title = "Voltage Regulator 7805", Price = 4000, MinQuantity = 10, IsActive = true, TypeId = 5 },
                new Product { Id = 8, Title = "ATmega328P-PU", Price = 58000, MinQuantity = 3, IsActive = true, TypeId = 6 },
                new Product { Id = 9, Title = "DS18B20 Temperature Sensor", Price = 25000, MinQuantity = 5, IsActive = true, TypeId = 7 },
                new Product { Id = 10, Title = "OLED Display 0.96 I2C", Price = 120000, MinQuantity = 2, IsActive = true, TypeId = 10 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Ali", LastName = "Khosravi" },
                new Customer { Id = 2, FirstName = "Reza", LastName = "Ghavami" },
                new Customer { Id = 3, FirstName = "Sina", LastName = "Ahmadi" },
                new Customer { Id = 4, FirstName = "Nima", LastName = "Karimi" },
                new Customer { Id = 5, FirstName = "Farhad", LastName = "Nikbakht" },
                new Customer { Id = 6, FirstName = "Ehsan", LastName = "Rahmani" },
                new Customer { Id = 7, FirstName = "Mina", LastName = "Sadeghi" },
                new Customer { Id = 8, FirstName = "Mahdi", LastName = "Hashemi" },
                new Customer { Id = 9, FirstName = "Parsa", LastName = "Ghanbari" },
                new Customer { Id = 10, FirstName = "Hassan", LastName = "Moradi" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Ehsan", LastName = "Arefzadeh" },
                new User { Id = 2, FirstName = "Reza", LastName = "Naghavi" },
                new User { Id = 3, FirstName = "Sina", LastName = "Moradi" }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, Type = TransactionType.Buy, DateTime = DateTime.Now.AddDays(-1), CustomerId = 1 },
                new Transaction { Id = 2, Type = TransactionType.Buy, DateTime = DateTime.Now.AddDays(-2), CustomerId = 2 },
                new Transaction { Id = 3, Type = TransactionType.Buy, DateTime = DateTime.Now.AddDays(-3), CustomerId = 3 },
                new Transaction { Id = 4, Type = TransactionType.Sell, DateTime = DateTime.Now.AddDays(-4), CustomerId = 4 },
                new Transaction { Id = 5, Type = TransactionType.Sell, DateTime = DateTime.Now.AddDays(-5), CustomerId = 5 },
                new Transaction { Id = 6, Type = TransactionType.Buy, DateTime = DateTime.Now.AddDays(-6), CustomerId = 6 },
                new Transaction { Id = 7, Type = TransactionType.Sell, DateTime = DateTime.Now.AddDays(-7), CustomerId = 7 },
                new Transaction { Id = 8, Type = TransactionType.Buy, DateTime = DateTime.Now.AddDays(-8), CustomerId = 8 },
                new Transaction { Id = 9, Type = TransactionType.Sell, DateTime = DateTime.Now.AddDays(-9), CustomerId = 9 },
                new Transaction { Id = 10, Type = TransactionType.Sell, DateTime = DateTime.Now.AddDays(-10), CustomerId = 10 }
            );

            modelBuilder.Entity<TransactionProduct>().HasData(

                new TransactionProduct { Id = 1, ProductId = 1, TransactionId = 1, Quantity = 1000, UnitPrice = 200 },
                new TransactionProduct { Id = 2, ProductId = 2, TransactionId = 1, Quantity = 800, UnitPrice = 150 },
                new TransactionProduct { Id = 3, ProductId = 3, TransactionId = 2, Quantity = 200, UnitPrice = 3000 },
                new TransactionProduct { Id = 4, ProductId = 4, TransactionId = 2, Quantity = 150, UnitPrice = 3500 },
                new TransactionProduct { Id = 5, ProductId = 5, TransactionId = 3, Quantity = 100, UnitPrice = 1200 },
                new TransactionProduct { Id = 6, ProductId = 6, TransactionId = 3, Quantity = 50, UnitPrice = 9000 },
                new TransactionProduct { Id = 7, ProductId = 1, TransactionId = 4, Quantity = 100, UnitPrice = 200 },
                new TransactionProduct { Id = 8, ProductId = 3, TransactionId = 5, Quantity = 20, UnitPrice = 3000 },
                new TransactionProduct { Id = 9, ProductId = 8, TransactionId = 6, Quantity = 20, UnitPrice = 58000 },
                new TransactionProduct { Id = 10, ProductId = 9, TransactionId = 6, Quantity = 40, UnitPrice = 25000 },
                new TransactionProduct { Id = 11, ProductId = 9, TransactionId = 7, Quantity = 5, UnitPrice = 25000 },
                new TransactionProduct { Id = 12, ProductId = 10, TransactionId = 8, Quantity = 10, UnitPrice = 120000 },
                new TransactionProduct { Id = 13, ProductId = 10, TransactionId = 9, Quantity = 1, UnitPrice = 120000 },
                new TransactionProduct { Id = 14, ProductId = 2, TransactionId = 10, Quantity = 50, UnitPrice = 150 }
            );
        }
    }
}
