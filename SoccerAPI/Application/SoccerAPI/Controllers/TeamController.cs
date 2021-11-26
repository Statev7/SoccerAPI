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

            if (team == null)
            {
				return this.NotFound();
            }

			return this.Ok(team);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			GetAllTeamsDTO teams = await this.teamService.GetAllAsync<GetAllTeamsDTO>();

			return this.Ok(teams);
		}

		[HttpPost]
		public async Task<IActionResult> Post(PostTeamDTO team)
		{
			Team createdTeam = await this.teamService.AddAsync(team);

			return this.CreatedAtRoute(this.RouteData, createdTeam);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Put(Guid id, PutTeamDTO team)
        {
			bool result = await this.teamService.UpdateAsync(id, team);

            if (result == false)
            {
				return this.BadRequest();
            }

			return this.Ok(result);
		}

		[HttpPatch]
		[Route("{id}")]
		public async Task<IActionResult> Patch(Guid id, PatchTeamDTO team)
		{
			bool result = await this.teamService.PartialUpdateAsync(id, team);

			return this.Ok(result);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			bool result = await this.teamService.DeleteAsync(id);

            if (result == false)
            {
				return this.BadRequest("Something went wrong!");
            }

			return this.Ok(result);
		}
	}
}
