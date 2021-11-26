namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Team;

    public interface ITeamService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<Team> AddAsync(PostTeamDTO team);

        Task<bool> UpdateAsync(Guid id, PutTeamDTO team);

        Task<bool> PartialUpdateAsync(Guid id, PatchTeamDTO team);

        Task<bool> DeleteAsync(Guid id);
    }
}
