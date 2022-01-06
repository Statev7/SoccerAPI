namespace SoccerAPI.Database.Seed.Seed
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Database.Models.Users;
    using SoccerAPI.DTOs.User;
    using SoccerAPI.Services.Database.Contracts;

    public class UserSeeder : BaseSeeder
    {
        public override async Task SeedAsync(IServiceScope serviceScope)
        {
            IUserService userService = (IUserService)serviceScope.ServiceProvider.GetRequiredService(typeof(IUserService));

            bool isThereAnyData = await userService.IsThereAnyDataAsync();
            if (isThereAnyData == false)
            {
                PostUserRegisterDTO user = new PostUserRegisterDTO()
                {
                    FirstName = GlobalConstants.USER_FIRST_NAME,
                    LastName = GlobalConstants.USER_LAST_NAME,
                    Email = GlobalConstants.USER_EMAIL,
                    Password = GlobalConstants.USER_PASSWORD,
                    RepeatPassword = GlobalConstants.USER_PASSWORD,
                };

                PostUserRegisterDTO editor = new PostUserRegisterDTO()
                {
                    FirstName = GlobalConstants.EDITOR_FIRST_NAME,
                    LastName = GlobalConstants.EDITOR_LAST_NAME,
                    Email = GlobalConstants.EDITOR_EMAIL,
                    Password = GlobalConstants.EDITOR_PASSWORD,
                    RepeatPassword = GlobalConstants.EDITOR_PASSWORD,
                };

                PostUserRegisterDTO admin = new PostUserRegisterDTO()
                {
                    FirstName = GlobalConstants.ADMIN_FIRST_NAME,
                    LastName = GlobalConstants.ADMIN_LAST_NAME,
                    Email = GlobalConstants.ADMIN_EMAIL,
                    Password = GlobalConstants.ADMIN_PASSWORD,
                    RepeatPassword = GlobalConstants.ADMIN_PASSWORD,
                };

                await userService.RegisterAsync<User>(user);
                await userService.RegisterAsync<User>(editor);
                await userService.RegisterAsync<User>(admin);
            }
        }
    }
}
