namespace SoccerAPI.Database.Seed.Seed.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    public interface IBaseSeeder
    {
        Task SeedAsync(IServiceScope serviceScope);
    }
}
