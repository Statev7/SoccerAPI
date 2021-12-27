namespace SoccerAPI.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SoccerAPI.Database.Models.Users;
    using SoccerAPI.DTOs.User;
    using SoccerAPI.Services.Database.Contracts;

    public class UserController : BaseAPIController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(PostUserLoginDTO model)
        {
            string token = await this.userService.LoginAsync(model);

            if (token == null)
            {
                throw new ArgumentException();
            }

            return this.Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(PostUserRegisterDTO model)
        {
            User user = await this.userService.RegisterAsync<User>(model);

            if (user == null)
            {
                throw new ArgumentException();
            }

            return this.Ok(user);
        }
    }
}
