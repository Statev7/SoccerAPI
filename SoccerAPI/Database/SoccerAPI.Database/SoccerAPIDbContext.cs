namespace SoccerAPI.Database
{
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Database.Models.Teams;

    public class SoccerAPIDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Coach> Coaches { get; set; }

        public DbSet<Footballer> Footballers { get; set; }

        public DbSet<Championship> Championships { get; set; }

        public DbSet<TeamFootballerMapping> TeamFootballerMapping { get; set; }

        public DbSet<TeamChampionshipMapping> TeamChampionshipMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseSqlServer("Server=.;Database=SoccerAPI;Integrated Security = true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
