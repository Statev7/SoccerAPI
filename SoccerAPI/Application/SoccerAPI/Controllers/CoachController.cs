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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            GetAllCoachesDTO coaches = await this.service.GetAllAsync<GetAllCoachesDTO>();

            return this.Ok(coaches);
        }

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(PostCoachDTO model)
        {
            Coach coach = await this.service.AddAsync(model);

            if (this.ModelState.IsValid == false)
            {
                var expections = this.ModelState.Values.SelectMany(e => e.Errors);
                throw new ModelException(expections);
            }

            return this.CreatedAtRoute(this.RouteData, coach);
        }

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
