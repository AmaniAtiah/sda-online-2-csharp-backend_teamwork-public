using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.EntityFrameWork
{
    public class AppDbContext1 : DbContext
    {
        public AppDbContext1(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) { }
    }
}