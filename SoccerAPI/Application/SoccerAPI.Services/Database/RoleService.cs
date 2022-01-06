namespace SoccerAPI.Services.Database
{
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Common.Exeptions;
    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Users;
    using SoccerAPI.Services.Database.Contracts;

    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(SoccerAPIDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {

        }

        public async Task<T> GetRoleByNameAsync<T>(string roleName)
        {
            Role role = await this.DbSet.SingleOrDefaultAsync(r => r.Name == roleName);

            if (role == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.ROLE_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            T mapped = this.Mapper.Map<T>(role);

            return mapped;
        }

        public async Task<T> AddAsync<T>(string roleName)
        {
            Role role = new Role()
            {
                Name = roleName,
            };

            await this.DbSet.AddAsync(role);
            await this.DbContext.SaveChangesAsync();

            T mapped = this.Mapper.Map<T>(role);
            return mapped;
        }

        public Task<bool> IsThereAnyDataAsync()
        {
            return this.DbSet.AnyAsync();
        }
    }
}
