namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Teams;

    public interface ITeamChampionshipMappingService
    {
        Task<bool> DeleteAsync(Guid championshipId, Guid teamId);

        Task<T> AddAsync<T>(TeamChampionshipMapping teamFootballerMapping);

        Task<T> GetByChampionshipAndTeamIdAsync<T>(Guid championshipId, Guid teamId);
    }
}
