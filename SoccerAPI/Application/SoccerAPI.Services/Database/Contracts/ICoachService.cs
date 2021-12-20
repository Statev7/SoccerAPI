namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Coach;

    public interface ICoachService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<Coach> AddAsync(PostCoachDTO model);

        Task<bool> UpdateAsync(Guid id, PutCoachDTO model);

        Task<bool> PartialUpdateAsync(Guid id, PatchCoachDTO model);

        Task<bool> DeleteAsync(Guid id);
    }
}
