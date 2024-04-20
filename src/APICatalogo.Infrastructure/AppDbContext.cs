using APICatalogo.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Infrastructure;

public class AppDbContext : IdentityDbContext<ApplicationUser> {
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext)
            .Assembly);

        builder.Entity<ApplicationUser>().ToTable("AspNetUsers");

        base.OnModelCreating(builder);

    }

}
