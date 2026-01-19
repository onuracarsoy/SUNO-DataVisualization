using DataVisualizationUI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataVisualizationUI.Context
{
    public class DataVisualizationDbContext(DbContextOptions<DataVisualizationDbContext> options) :  DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}

