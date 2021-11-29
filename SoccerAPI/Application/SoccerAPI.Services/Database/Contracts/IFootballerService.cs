namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Footballer;

    public interface IFootballerService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<Footballer> AddAsync(PostFootballerDTO footballer);

        Task<bool> UpdateAsync(Guid id, PutFootballerDTO footballer);

        Task<bool> PartialUpdateAsync(Guid id, PatchFootballerDTO footballer);

        Task<bool> DeleteAsync(Guid id);
    }
}
