using EmpleadosApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosApp.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .Property(e => e.Nombre)
            .HasMaxLength(100);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Cargo)
            .HasMaxLength(60);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Telefono)
            .HasMaxLength(20);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Departamento)
            .HasMaxLength(100);

        base.OnModelCreating(modelBuilder);
    }
}
