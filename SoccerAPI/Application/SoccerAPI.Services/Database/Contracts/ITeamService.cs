namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Team;

    public interface ITeamService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<Team> AddAsync(PostTeamDTO team);

        Task<bool> DeleteAsync(Guid id);
    }
}
