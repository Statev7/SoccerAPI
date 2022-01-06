namespace SoccerAPI.Database.Seed
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using SoccerAPI.Database.Seed.Seed;
    using SoccerAPI.Database.Seed.Seed.Contracts;

    public static class Launcher
    {
        public static async Task SeedDatabaseAsync(IApplicationBuilder app)
        {
            ICollection<IBaseSeeder> seeders = new List<IBaseSeeder>
            {
                new RoleSeeder(),
                new UserSeeder(),
                new UserRoleSeeder(),
            };

            using (var serviceProvider = app.ApplicationServices.CreateScope())
            {
                foreach (var seeder in seeders)
                {
                    await seeder.SeedAsync(serviceProvider);
                }
            }
        }
    }
}
