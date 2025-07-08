using ApiDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Department> Departments { get; set; }
}
