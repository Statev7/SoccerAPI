namespace SoccerAPI.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Common.Exeptions;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Team;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamController : BaseAPIController
    {
        private readonly ITeamService teamService;
        private readonly ITeamFootballerMappingService teamFootballerMappingService;
        private readonly ITeamCoachService teamCoachService;

        public TeamController(
            ITeamService teamService, 
            ITeamFootballerMappingService teamFootballerMappingService,
            ITeamCoachService teamCoachService)
        {
            this.teamService = teamService;
            this.teamFootballerMappingService = teamFootballerMappingService;
            this.teamCoachService = teamCoachService;
        }

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns>Returns all teams sorted by name</returns>
        /// <response code="200">Returns all teams</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            GetAllTeamsDTO teams = await this.teamService.GetAllAsync<GetAllTeamsDTO>();

            return this.Ok(teams);
        }

        /// <summary>
        ///  Get team by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Returns the team entity by the given id</returns>
        /// <response code="200">Returns the team entity by the given id</response>
        /// <response code="404">If the team is null</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            GetTeamDTO team = await this.teamService.GetByIdAsync<GetTeamDTO>(id);

            if (team == null)
            {
                return this.NotFound();
            }

            return this.Ok(team);
        }

        /// <summary>
        /// Create team
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Team
        ///     {
        ///        "name": "TeamName",
        ///        "nickname": "TeamNickname",
        ///        "stadium": "TeamStadium",
        ///        "country": "TeamCountry"
        ///     }
        ///
        /// </remarks>
        /// <param name="team">Body model with data</param>
        /// <returns>The team that is created</returns>
        /// <response code="200">If the team is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(PostTeamDTO team)
        {
            Team createdTeam = await this.teamService.AddAsync(team);

            return this.CreatedAtRoute(this.RouteData, createdTeam);
        }

        /// <summary>
        /// Update team
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/Team
        ///     {
        ///        "name": "TeamName",
        ///        "nickname": "TeamNickname",
        ///        "stadium": "TeamStadium",
        ///        "country": "TeamCountry"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The team id</param>
        /// <param name="team">Body model with data to update</param>
        /// <returns>The result from the update action</returns>
        /// <response code="200">If the team is updated successfully</response>
        /// <response code="400">If the team is not correct</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, PutTeamDTO team)
        {
            bool result = await this.teamService.UpdateAsync(id, team);

            if (result == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Partial update team
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/Team
        ///     {
        ///        "name": "TeamName",
        ///        "nickname": "TeamNickname",
        ///        "stadium": "TeamStadium",
        ///        "country": "TeamCountry"
        ///        "footballersId: [
        ///             "FootballerId"
        ///        ],
        ///        "coachesId: [
        ///             "CoachId"
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The team id</param>
        /// <param name="team">Body model with data to partial update</param>
        /// <returns>The result from the update action</returns>
        /// <response code="200">If the team is updated successfully</response>
        /// <response code="400">If the team is not correct</response>
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(Guid id, PatchTeamDTO team)
        {
            bool result = await this.teamService.PartialUpdateAsync(id, team);

            if (this.ModelState.IsValid == false)
            {
                var expections = this.ModelState.Values.SelectMany(e => e.Errors);
                throw new ModelException(expections);
            }

            if (result == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

        /// <summary>
		/// Delete team by Id
		/// </summary>
		/// <param name="id">The team id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the team is deleted successfully</response>
		/// <response code="400">If the team is null</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result = await this.teamService.DeleteAsync(id);

            if (result == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

        /// <summary>
		/// Delete footballer from team
		/// </summary>
		/// <param name="teamId">The team id</param>
		/// <param name="footballerId">The footballer id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the relation is deleted successfully</response>
		/// <response code="400">If there is no relation</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid teamId, Guid footballerId)
        {
            bool result = await this.teamFootballerMappingService.DeleteAsync(teamId, footballerId);

            return this.Ok(result);
        }

        /// <summary>
		/// Delete coach from team
		/// </summary>
		/// <param name="teamId">The team id</param>
		/// <param name="coachId">The coach id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the relation is deleted successfully</response>
		/// <response code="400">If there is no relation</response>
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCoach(Guid teamId, Guid coachId)
        {
            bool result = await this.teamCoachService.DeleteAsync(teamId, coachId);

            return this.Ok(result);
        }
    }
}
