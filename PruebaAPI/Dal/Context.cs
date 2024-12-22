using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace PruebaAPI.Dal;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }
    
    public DbSet<Garantias> Garantias { get; set; }
    public DbSet<Compras> Compras { get; set; }
    public DbSet<ComprasDetalle> ComprasDetalle { get; set; }

    public DbSet<Categorias> Categorias { get; set; }

    public DbSet<Productos> Productos { get; set; }
    public DbSet<Proveedores> Proveedores { get; set; }


}