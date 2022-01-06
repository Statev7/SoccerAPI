namespace SoccerAPI.Database.Seed.Seed
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Database.Models.Users;
    using SoccerAPI.Services.Database.Contracts;

    public class RoleSeeder : BaseSeeder
    {
        public override async Task SeedAsync(IServiceScope serviceScope)
        {
            IRoleService roleService = (IRoleService)serviceScope.ServiceProvider.GetRequiredService(typeof(IRoleService));

            bool isThereAnyData = await roleService.IsThereAnyDataAsync();
            if (isThereAnyData == false)
            {
                await roleService.AddAsync<Role>(GlobalConstants.USER_ROLE_NAME);
                await roleService.AddAsync<Role>(GlobalConstants.EDITOR_ROLE_NAME);
                await roleService.AddAsync<Role>(GlobalConstants.ADMIN_ROLE_NAME);
            }
        }
    }
}
