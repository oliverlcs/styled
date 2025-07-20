using Microsoft.EntityFrameworkCore;
using Styled.Api.Models;

namespace Styled.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        // Later:
        // public DbSet<ClothingItem> ClothingItems { get; set; }
        // public DbSet<Outfit> Outfits { get; set; }
    }
}
