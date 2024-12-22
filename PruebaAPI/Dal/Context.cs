using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace SistemaLlavesWebAPI.Dal;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }
    
    public DbSet<Garantias> Garantias { get; set; }
  
}