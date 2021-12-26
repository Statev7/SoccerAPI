namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Championships;
    using SoccerAPI.DTOs.Championship;

    public interface IChampionshipService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<Championship> AddAsync(PostChampionshipDTO championship);

        Task<bool> UpdateAsync(Guid id, PutChampionshipDTO championship);

        Task<bool> PartialUpdateAsync(Guid id, PatchChampionshipDTO championship);

        Task<bool> DeleteAsync(Guid id);
    }
}
