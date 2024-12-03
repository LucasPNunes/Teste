using Microsoft.EntityFrameworkCore;
using VendasLitoral.Domain.Models;

namespace VendasLitoral.Infrastructure.Context;

public class ConnectionContext : DbContext
{
    public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
    {
        
    }
    
    public DbSet<Pedido> PEDIDO { get; set; }
    public DbSet<Vendedor> VENDEDOR { get; set; }
    public DbSet<Cliente> CLIENTE { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vendedor>()
            .HasIndex(u => u.CodigoVendedor)
            .IsUnique();
        modelBuilder.Entity<Cliente>()
            .HasIndex(u => u.CNPJ)
            .IsUnique();
    }
}