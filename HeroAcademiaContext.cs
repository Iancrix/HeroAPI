using HeroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroAPI
{
    public class HeroAcademiaContext : DbContext
    {
        public HeroAcademiaContext(DbContextOptions<HeroAcademiaContext> options) : base(options)
        {

        }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<HeroSidekick> Sidekicks { get; set; }
    }
}