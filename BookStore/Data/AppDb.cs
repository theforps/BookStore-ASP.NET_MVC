using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> options): base(options)
    {
        Database.EnsureCreated();
    }


    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Company> Companies { get; set; }



}