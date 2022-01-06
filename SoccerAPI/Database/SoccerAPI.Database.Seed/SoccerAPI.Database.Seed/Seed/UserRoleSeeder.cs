namespace SoccerAPI.Database.Seed.Seed
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.DTOs.Role;
    using SoccerAPI.DTOs.User;
    using SoccerAPI.Services.Database.Contracts;

    public class UserRoleSeeder : BaseSeeder
    {
        public override async Task SeedAsync(IServiceScope serviceScope)
        {
            IUserRoleMappingService userRoleService = (IUserRoleMappingService)serviceScope
                .ServiceProvider
                .GetRequiredService(typeof(IUserRoleMappingService));

            IUserService userService = (IUserService)serviceScope.ServiceProvider.GetRequiredService(typeof(IUserService));
            IRoleService roleService = (IRoleService)serviceScope.ServiceProvider.GetRequiredService(typeof(IRoleService));

            bool isThereAnyData = await userRoleService.HasTheSeedAlreadyPassedAsync();
            if (isThereAnyData == false)
            {
                GetUserIdDTO user = await userService.GetUserByEmailAsync<GetUserIdDTO>(GlobalConstants.USER_EMAIL);
                GetUserIdDTO  editor = await userService.GetUserByEmailAsync<GetUserIdDTO>(GlobalConstants.EDITOR_EMAIL);
                GetUserIdDTO admin = await userService.GetUserByEmailAsync<GetUserIdDTO>(GlobalConstants.ADMIN_EMAIL);

                GetRoleIdDTO userRole = await roleService.GetRoleByNameAsync<GetRoleIdDTO>(GlobalConstants.USER_ROLE_NAME);
                GetRoleIdDTO editorRole = await roleService.GetRoleByNameAsync<GetRoleIdDTO>(GlobalConstants.EDITOR_ROLE_NAME);
                GetRoleIdDTO adminRole = await roleService.GetRoleByNameAsync<GetRoleIdDTO>(GlobalConstants.ADMIN_ROLE_NAME);

                await userRoleService.SetRoleAsync(user.Id, userRole.Id);
                await userRoleService.SetRoleAsync(editor.Id, editorRole.Id);
                await userRoleService.SetRoleAsync(admin.Id, adminRole.Id);
            }
        }
    }
}
