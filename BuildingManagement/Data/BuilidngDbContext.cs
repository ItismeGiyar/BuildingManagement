using BuildingManagement.Models;
using Microsoft.EntityFrameworkCore;
namespace BuildingManagement.Data
{
    public class BuildingDbContext : DbContext
    {
        public BuildingDbContext(DbContextOptions<BuildingDbContext> options) : base(options) { }

        public DbSet<Company> MS_Company { get; set; }
        public DbSet<RoomProperty> MS_PropertyRoom { get; set; } = default!;
    }
}

