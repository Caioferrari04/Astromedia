using Astromedia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AstroContext : IdentityDbContext<Usuario>
{
    public AstroContext(DbContextOptions<AstroContext> options) : base(options)
    { }

    public DbSet<Postagem> Postagens { get; set; }
}