namespace SoccerAPI.Services.Database.Contracts
{
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Teams;

    public interface ITeamChampionshipMappingService
    {
        Task<T> AddAsync<T>(TeamChampionshipMapping teamFootballerMapping);
    }
}
