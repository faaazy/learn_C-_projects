using Microsoft.EntityFrameworkCore;
using plzwork.Models;

namespace plzwork.Data;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Todo> Todos { get; set;}
}