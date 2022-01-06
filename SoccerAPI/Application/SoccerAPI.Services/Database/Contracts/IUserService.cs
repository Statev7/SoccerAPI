namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    using SoccerAPI.DTOs.User;

    public interface IUserService
    {
        Task<T> RegisterAsync<T>(PostUserRegisterDTO model);

        Task<string> LoginAsync(PostUserLoginDTO model);

        Task<T> GetByIdAsync<T>(Guid id);

        Task<T> GetUserByEmailAsync<T>(string email);

        string GenerateSalt();

        string HashPassword(string password, string passwordSalt);

        Task<bool> IsThereAnyDataAsync();
    }
}
