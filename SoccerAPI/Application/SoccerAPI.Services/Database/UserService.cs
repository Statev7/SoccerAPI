namespace SoccerAPI.Services.Database
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using SoccerAPI.Common;
    using SoccerAPI.Common.Constants;
    using SoccerAPI.Common.Exeptions;
    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Users;
    using SoccerAPI.DTOs.User;
    using SoccerAPI.Services.Database.Contracts;

    public class UserService : BaseService<User>, IUserService
    {
        private readonly ApplicationSettings options;
        private readonly IRoleService roleService;
        private readonly IUserRoleMappingService userRoleMappingService;

        public UserService(
            SoccerAPIDbContext dbContext, 
            IMapper mapper, 
            IOptions<ApplicationSettings> options,
            IRoleService roleService,
            IUserRoleMappingService userRoleMappingService)
            : base(dbContext, mapper)
        {
            this.options = options.Value;
            this.roleService = roleService;
            this.userRoleMappingService = userRoleMappingService;
        }

        public async Task<T> GetUserByEmailAsync<T>(string email)
        {
            User user = await this.DbSet
                .Include(u => u.Roles)
                .ThenInclude(urm => urm.Role)
                .SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.USER_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            T mapped = this.Mapper.Map<T>(user);

            return mapped;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            User user = await this.DbSet
                .Include(u => u.Roles)
                .ThenInclude(urm => urm.Role)
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.USER_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            T mapped = this.Mapper.Map<T>(user);

            return mapped;
        }

        public async Task<string> LoginAsync(PostUserLoginDTO model)
        {
            User user = await this.GetUserByEmailAsync<User>(model.Email);

            string enteredHashedPassword = this.HashPassword(model.Password, user.Salt);
            if (user.PasswordHash != enteredHashedPassword)
            {
                throw new ArgumentException(ExceptionMessages.INVALID_PASSWORD_ERROR_MESSAGE);
            }

            string jwtToken = this.GenerateJwtToken(user.Id.ToString());

            return jwtToken;
        }

        public async Task<T> RegisterAsync<T>(PostUserRegisterDTO model)
        {
            User user = await this.DbSet
                .SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                throw new ArgumentException(ExceptionMessages.EMAIL_ALREADY_REGISTERED_ERROR_MESSAGE);
            }

            User userToRegister = this.Mapper.Map<User>(model);

            userToRegister.Salt = this.GenerateSalt();
            userToRegister.PasswordHash = this.HashPassword(model.Password, userToRegister.Salt);

            await this.DbSet.AddAsync(userToRegister);
            await this.DbContext.SaveChangesAsync();

            Role role = await this.roleService.GetRoleByNameAsync<Role>(GlobalConstants.USER_ROLE_NAME);
            Guid roleId = role.Id;

            await this.userRoleMappingService.AddRoleToUserAsync(roleId, userToRegister.Id);

            T mapped = this.Mapper.Map<T>(userToRegister);
            return mapped;
        }

        public Task<bool> IsThereAnyDataAsync()
        {
            return this.DbSet.AnyAsync();
        }

        public string GenerateSalt()
        {
            byte[] saltArray = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltArray);
            }

            string salt = Encoding.UTF8.GetString(saltArray);

            return salt;
        }

        public string HashPassword(string password, string passwordSalt)
        {
            var salt = Encoding.ASCII.GetBytes(passwordSalt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return hashed;
        }

        private string GenerateJwtToken(string userId)
        {
            // generate token that is valid for 2 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.JwtApiSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                   new Claim("id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials =
                            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
