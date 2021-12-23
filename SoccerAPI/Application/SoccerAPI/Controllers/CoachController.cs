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
    using SoccerAPI.DTOs.Coach;
    using SoccerAPI.Services.Database.Contracts;

    public class CoachController : BaseAPIController
    {
        private readonly ICoachService service;

        public CoachController(ICoachService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get all coaches
        /// </summary>
        /// <returns>Returns all coaches</returns>
        /// <response code="200">Returns all coaches</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            GetAllCoachesDTO coaches = await this.service.GetAllAsync<GetAllCoachesDTO>();

            return this.Ok(coaches);
        }

        /// <summary>
        ///  Get coache by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Returns the coache entity by the given id</returns>
        /// <response code="200">Returns the coache entity by the given id</response>
        /// <response code="404">If the coache is null</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            GetCoachDTO coach = await this.service.GetByIdAsync<GetCoachDTO>(id);

            if (coach == null)
            {
                return this.NotFound();
            }

            return this.Ok(coach);
        }

        /// <summary>
        /// Create coach
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Coach
        ///     {
        ///        "firstName": "CoachFirstName",
        ///        "secondName": "CoachSecondName",
        ///        "nationality": "CoachNationality",
        ///        "dateOfBirth": "CoachDateOfBirth",
        ///        "salary": "CoachSalary",
        ///        "coachType": "CoachType"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Body model with data</param>
        /// <returns>The team that is created</returns>
        /// <response code="200">If the coach is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(PostCoachDTO model)
        {
            //TODO Add validation for age!
            Coach coach = await this.service.AddAsync(model);

            if (this.ModelState.IsValid == false)
            {
                var expections = this.ModelState.Values.SelectMany(e => e.Errors);
                throw new ModelException(expections);
            }

            return this.CreatedAtRoute(this.RouteData, coach);
        }

        /// <summary>
        /// Update coach
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/Coach
        ///     {
        ///        "firstName": "CoachFirstName",
        ///        "secondName": "CoachSecondName",
        ///        "nationality": "CoachNationality",
        ///        "dateOfBirth": "CoachDateOfBirth",
        ///        "salary": "CoachSalary",
        ///        "coachType": "CoachType"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The coach id</param>
        /// <param name="model">Body model with data to update</param>
        /// <returns>The result from the update action</returns>
        /// <response code="200">If the coach is updated successfully</response>
        /// <response code="400">If the coach is not correct</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, PutCoachDTO model)
        {
            bool result = await this.service.UpdateAsync(id, model);

            if (result == false)
            {
                this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Partial update coach
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Patch /api/Coach
        ///     {
        ///        "firstName": "CoachFirstName",
        ///        "secondName": "CoachSecondName",
        ///        "nationality": "CoachNationality",
        ///        "dateOfBirth": "CoachDateOfBirth",
        ///        "salary": "CoachSalary",
        ///        "coachType": "CoachType"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The coach id</param>
        /// <param name="model">Body model with data to update</param>
        /// <returns>The result from the update action</returns>
        /// <response code="200">If the coach is updated successfully</response>
        /// <response code="400">If the coach is not correct</response>
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(Guid id, PatchCoachDTO model)
        {
            bool result = await this.service.PartialUpdateAsync(id, model);

            if (result == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

        /// <summary>
		/// Delete coach by Id
		/// </summary>
		/// <param name="id">The coach id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the coach is deleted successfully</response>
		/// <response code="400">If the coach is null</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result = await this.service.DeleteAsync(id);

            if (result == false)
            {
                this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

    }
}
