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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            GetAllFootballersDTO footballers = await this.footballerService.GetAllAsync<GetAllFootballersDTO>();

            return this.Ok(footballers);
        }

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(PostFootballerDTO footballer)
        {
            Footballer footballerToCreate = await this.footballerService.AddAsync(footballer);

            return this.CreatedAtRoute(this.RouteData, footballerToCreate);
        }

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

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(Guid id, PatchFootballerDTO footballer)
        {
            bool result = await this.footballerService.PartialUpdateAsync(id, footballer);

            return this.Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
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
