namespace SoccerAPI.Services.Database.Contracts
{
    using System.Threading.Tasks;

    public interface IRoleService
    {
        Task<T> GetRoleByNameAsync<T>(string roleName);

        Task<T> AddAsync<T>(string roleName);

        Task<bool> IsThereAnyDataAsync();
    }
}
