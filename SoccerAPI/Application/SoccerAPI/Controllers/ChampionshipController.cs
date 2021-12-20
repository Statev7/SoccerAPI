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

        public ChampionshipController(IChampionshipService championshipService)
        {
            this.championshipService = championshipService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            GetAllChampionshipsDTO championshoips = await this.championshipService.GetAllAsync<GetAllChampionshipsDTO>();

            return this.Ok(championshoips);
        }

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(PostChampionshipDTO model)
        {
            Championship championship = await this.championshipService.AddAsync(model);

            return this.CreatedAtRoute(this.RouteData, championship);
        }

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
    }
}
