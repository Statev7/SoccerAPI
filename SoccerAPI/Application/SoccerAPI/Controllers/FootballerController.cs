namespace SoccerAPI.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Footballer;
    using SoccerAPI.Services.Database.Contracts;

    public class FootballerController : BaseAPIController
    {
        private readonly IFootballerService footballerService;

        public FootballerController(IFootballerService footballerService)
        {
            this.footballerService = footballerService;
        }

        /// <summary>
        /// Get all footballers
        /// </summary>
        /// <returns>Returns all footballers</returns>
        /// <response code="200">Returns all footballers</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            GetAllFootballersDTO footballers = await this.footballerService.GetAllAsync<GetAllFootballersDTO>();

            return this.Ok(footballers);
        }

        /// <summary>
        ///  Get footballer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <returns>Returns the footballer entity by the given id</returns>
        /// <response code="200">Returns the footballer entity by the given id</response>
        /// <response code="404">If the footballer is null</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            GetFootballerDTO footballer = await this.footballerService.GetByIdAsync<GetFootballerDTO>(id);

            if (footballer == null)
            {
                return this.NotFound();
            }

            return this.Ok(footballer);
        }

        /// <summary>
        /// Create footballer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Footballer
        ///     {
        ///        "firstName": "FootballerFirstName",
        ///        "secondName": "FootballerSecondName",
        ///        "nationality": "FootballerNationality",
        ///        "dateOfBirth": "FootballerDateOfBirth",
        ///        "salary": "FootballerSalary",
        ///        "position": "FootballerPosition",
        ///        "strongLeg": "FootballerStrongLeg"
        ///     }
        ///
        /// </remarks>
        /// <param name="footballer">Body model with data</param>
        /// <returns>The footballer that is created</returns>
        /// <response code="200">If the footballer is created successfully</response>
        /// <response code="400">If the body is not correct</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(PostFootballerDTO footballer)
        {
            Footballer footballerToCreate = await this.footballerService.AddAsync(footballer);

            return this.CreatedAtRoute(this.RouteData, footballerToCreate);
        }

        /// <summary>
        /// Update footballer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/Footballer
        ///     {
        ///        "firstName": "FootballerFirstName",
        ///        "secondName": "FootballerSecondName",
        ///        "nationality": "FootballerNationality",
        ///        "dateOfBirth": "FootballerDateOfBirth",
        ///        "salary": "FootballerSalary",
        ///        "position": "FootballerPosition",
        ///        "strongLeg": "FootballerStrongLeg"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The footballer id</param>
        /// <param name="footballer">Body model with data to update</param>
        /// <returns>The result from the update action</returns>
        /// <response code="200">If the footballer is updated successfully</response>
        /// <response code="400">If the footballer is not correct</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, PutFootballerDTO footballer)
        {
            bool result = await this.footballerService.UpdateAsync(id, footballer);

            if (result == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Partial update footballer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Patch /api/Footballer
        ///     {
        ///        "firstName": "FootballerFirstName",
        ///        "secondName": "FootballerSecondName",
        ///        "nationality": "FootballerNationality",
        ///        "dateOfBirth": "FootballerDateOfBirth",
        ///        "salary": "FootballerSalary",
        ///        "position": "FootballerPosition",
        ///        "strongLeg": "FootballerStrongLeg"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The footballer id</param>
        /// <param name="footballer">Body model with data to update</param>
        /// <returns>The result from the update action</returns>
        /// <response code="200">If the footballer is updated successfully</response>
        /// <response code="400">If the footballer is not correct</response>
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(Guid id, PatchFootballerDTO footballer)
        {
            bool result = await this.footballerService.PartialUpdateAsync(id, footballer);

            return this.Ok(result);
        }

        /// <summary>
		/// Delete team by Id
		/// </summary>
		/// <param name="id">The footballer id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the footballer is deleted successfully</response>
		/// <response code="400">If the footballer is null</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result =  await this.footballerService.DeleteAsync(id);

            if (result == false)
            {
                return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
            }

            return this.Ok(result);
        }
    }
}
