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

        /// <summary>
        /// Login in
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Login
        ///     {
        ///        "email": "user@email.com",
        ///        "password": "userpassword"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns></returns>
        /// <returns>Returns the jtwtoken</returns>
        /// <response code="200">Returns the jtwtoken</response>
        /// <response code="404">If the user does not exist</response>
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

        /// <summary>
        /// Register
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Register
        ///     {
        ///        "firstName": "userFirstName",
        ///        "lastName: "userLastName",
        ///        "email": "user@email.com",
        ///        "password": "password",
        ///        "repeatPassword": "password"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns></returns>
        /// <response code="200">If the registration is successful</response>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(PostUserRegisterDTO model)
        {
            GetUserInformationDTO user = await this.userService.RegisterAsync<GetUserInformationDTO>(model);

            if (user == null)
            {
                throw new ArgumentException();
            }

            return this.Ok(user);
        }
    }
}
