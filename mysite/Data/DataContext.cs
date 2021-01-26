using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mysite.Models;
using mysite.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Data
{
    public class DataContext : IdentityDbContext
    {
    public DataContext(DbContextOptions<DataContext> options) : base (options){ }
    public DbSet<AppUser> appUsers { get; set; }
    public DbSet<MyMessage> MyMessages { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public DbSet<MainComment> MainComments { get; set;  }
    public DbSet<SubComment> SubComments { get; set; }
    public DbSet<Home> Home { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<Truck> Trucks { get; set; }
    public DbSet<Techline> Techlines { get; set; }
    public DbSet<Warranty> Warranties { get; set; }

    }
}
