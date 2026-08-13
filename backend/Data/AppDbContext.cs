using Microsoft.EntityFrameworkCore;
using plzwork.Models;

namespace plzwork.Data;

public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set;}
}