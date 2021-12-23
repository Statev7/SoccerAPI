namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Teams;

    public interface ITeamFootballerMappingService
    {

        Task<T> GetByTeamAndFootballerIdAsync<T>(Guid teamId, Guid footbollerId);

        Task<T> AddAsync<T>(TeamFootballerMapping teamFootballerMapping);

        Task<bool> DeleteAsync(Guid teamId, Guid footballerId);
    }
}
