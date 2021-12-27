namespace SoccerAPI.Database
{
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using SoccerAPI.Common;
    using SoccerAPI.Database.Models.Championships;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.Database.Models.Users;

    public class SoccerAPIDbContext : DbContext
    {
        private readonly ApplicationSettings options;

        public SoccerAPIDbContext(IOptions<ApplicationSettings> options)
        {
            this.options = options.Value;
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Coach> Coaches { get; set; }

        public DbSet<Footballer> Footballers { get; set; }

        public DbSet<Championship> Championships { get; set; }

        public DbSet<TeamFootballerMapping> TeamFootballerMapping { get; set; }

        public DbSet<ChampionshipTeamMapping> ChampionshipTeamMapping { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoleMapping> UserRoleMapping { get; set; }

        public DbSet<TeamUserMapping> TeamUserMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseSqlServer(options.DbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
