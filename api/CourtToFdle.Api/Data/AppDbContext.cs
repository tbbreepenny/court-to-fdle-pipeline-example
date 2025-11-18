using Microsoft.EntityFrameworkCore;
using CourtToFdle.Api.Models;

namespace CourtToFdle.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<FdleCase> FdleCases => Set<FdleCase>();
    }
}

