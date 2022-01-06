namespace SoccerAPI.Services.Database
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Users;
    using SoccerAPI.Services.Database.Contracts;

    public class UserRoleMappingService : BaseService<UserRoleMapping>, IUserRoleMappingService
    {
        public UserRoleMappingService( SoccerAPIDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {

        }

        public async Task AddRoleToUserAsync(Guid roleId, Guid userId)
        {
            UserRoleMapping userRoleMapping = new UserRoleMapping()
            {
                RoleId = roleId,
                UserId = userId,
            };

            await this.DbSet.AddAsync(userRoleMapping);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task SetRoleAsync(Guid userId, Guid roleId)
        {
            var user = await this.DbSet.SingleOrDefaultAsync(x => x.UserId == userId);
            user.RoleId = roleId;

            this.DbSet.Update(user);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<bool> HasTheSeedAlreadyPassedAsync()
        {
            var user = await this.DbSet.SingleOrDefaultAsync(x => x.User.Email == GlobalConstants.USER_EMAIL);
            var admin = await this.DbSet.SingleOrDefaultAsync(x => x.User.Email == GlobalConstants.ADMIN_EMAIL);

            return user.RoleId != admin.RoleId;
        }
    }
}
