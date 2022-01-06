namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRoleMappingService
    {
        Task AddRoleToUserAsync(Guid roleId, Guid userId);

        Task SetRoleAsync(Guid userId, Guid roleId);

        Task<bool> HasTheSeedAlreadyPassedAsync();
    }
}
