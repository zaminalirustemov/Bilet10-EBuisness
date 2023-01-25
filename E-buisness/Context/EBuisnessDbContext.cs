using E_buisness.Data;
using E_buisness.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_buisness.Context
{
    public class EBuisnessDbContext:IdentityDbContext
    {
        public EBuisnessDbContext(DbContextOptions<EBuisnessDbContext> options) : base(options) { }

        public DbSet<Position> Positions { get; set; }
        public DbSet<SpecialTeam> SpecialTeams { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}
