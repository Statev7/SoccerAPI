namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Teams;

    public interface ITeamFootballerMappingService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<T> AddAsync<T>(TeamFootballerMapping teamFootballerMapping);

        Task<bool> DeleteAsync(Guid id);
    }
}
