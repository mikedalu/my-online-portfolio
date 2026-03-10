using System;
using Microsoft.EntityFrameworkCore;
namespace my_online_portfolio.Models;

public class MyDbContext : DbContext
{
  public MyDbContext(DbContextOptions<MyDbContext> options)
      : base(options) { }

  public DbSet<Gallery> Gallery { get; set; }
}
