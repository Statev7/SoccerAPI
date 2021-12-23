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
    using SoccerAPI.DTOs.Championship;
    using SoccerAPI.Services.Database.Contracts;

    public class ChampionshipController : BaseAPIController
    {
        private readonly IChampionshipService championshipService;
        private readonly ITeamChampionshipMappingService teamChampionshipMappingService;

        public ChampionshipController(
            IChampionshipService championshipService, 
            ITeamChampionshipMappingService teamChampionshipMappingService)
        {
            this.championshipService = championshipService;
            this.teamChampionshipMappingService = teamChampionshipMappingService;
        }

        /// <summary>
        /// Get all championships
        /// </summary>
        /// <returns>Returns all championships</returns>
        /// <response code="200">Returns all championships</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            GetAllChampionshipsDTO championshoips = await this.championshipService.GetAllAsync<GetAllChampionshipsDTO>();

            return this.Ok(championshoips);
        }

        /// <summary>
        ///  Get championship by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Returns the championship entity by the given id</returns>
        /// <response code="200">Returns the championship entity by the given id</response>
        /// <response code="404">If the championship is null</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            GetChampionshipDTO championship = await this.championshipService.GetByIdAsync<GetChampionshipDTO>(id);

            if (championship == null)
            {
                return this.NotFound();
            }

            return this.Ok(championship);
        }

        /// <summary>
        /// Create championship
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Championship
        ///     {
        ///        "name": "ChampionshipName"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns>The championship that is created</returns>
        /// <response code="200">If the championship is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(PostChampionshipDTO model)
        {
            Championship championship = await this.championshipService.AddAsync(model);

            return this.CreatedAtRoute(this.RouteData, championship);
        }

        /// <summary>
        /// Update championship
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/Championship
        ///     {
        ///        "name": "ChampionshipName"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The championship id</param>
        /// <param name="model">Body model with data to update</param>
        /// <returns>The result from the update action</returns>
        /// <response code="200">If the championship is updated successfully</response>
        /// <response code="400">If the championship is not correct</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, PutChampionshipDTO model)
        {
            bool result = await this.championshipService.UpdateAsync(id, model);

            if (result == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Partial update championship
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/Championship
        ///     {
        ///        "name": "ChampionshipName"
        ///        "teamsId: [
        ///             "TeamId"
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The championship id</param>
        /// <param name="model">Body model with data to partial update</param>
        /// <returns>The result from the update action</returns>
        /// <response code="200">If the championship is updated successfully</response>
        /// <response code="400">If the championship is not correct</response>
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(Guid id, PatchChampionshipDTO model)
        {
            bool result = await this.championshipService.PartialUpdateAsync(id, model);

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
        /// Delete championship by Id
        /// </summary>
        /// <param name="id">The championship id</param>
        /// <returns>The result from the delete action</returns>
        /// <response code="200">If the championship is deleted successfully</response>
        /// <response code="400">If the championship is null</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result = await this.championshipService.DeleteAsync(id);

            if (result == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

        /// <summary>
		/// Delete team from championship
		/// </summary>
		/// <param name="championshipId">The championship id</param>
		/// <param name="teamId">The team id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the relation is deleted successfully</response>
		/// <response code="400">If there is no relation</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid championshipId, Guid teamId)
        {
            bool result = await this.teamChampionshipMappingService.DeleteAsync(championshipId, teamId);

            return this.Ok(result);
        }
    }
}
