namespace SoccerAPI.Database.Seed.Seed
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using SoccerAPI.Database.Seed.Seed.Contracts;

    public abstract class BaseSeeder : IBaseSeeder
    {
        public abstract Task SeedAsync(IServiceScope serviceScope);
    }
}
