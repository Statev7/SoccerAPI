namespace SoccerAPI.Controllers
{
    using System;
    using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Team;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamController : BaseAPIController
    {
		private readonly ITeamService teamService;

        public TeamController(ITeamService teamService)
        {
			this.teamService = teamService;
        }

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			GetTeamDTO team = await this.teamService.GetByIdAsync<GetTeamDTO>(id);

			return this.Ok(team);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			GetAllTeamsDTO teams = await this.teamService.GetAllAsync<GetAllTeamsDTO>();

			return this.Ok(teams);
		}

		[HttpPost]
		public async Task<IActionResult> Post(PostTeamDTO model)
		{
			Team createdTeam = await this.teamService.AddAsync(model);

			return this.CreatedAtRoute(this.RouteData, createdTeam);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			bool isDeleted = await this.teamService.DeleteAsync(id);

			return this.Ok(isDeleted);
		}
	}
}
