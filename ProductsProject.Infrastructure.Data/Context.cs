using Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsProject.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt) { }
        
           public DbSet<Product> Products { get; set; }
    } 
}
