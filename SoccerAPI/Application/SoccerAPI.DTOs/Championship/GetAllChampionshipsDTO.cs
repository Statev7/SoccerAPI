namespace SoccerAPI.DTOs.Championship
{
    using System.Collections.Generic;
    using System.Linq;

    public class GetAllChampionshipsDTO
    {
        public IEnumerable<GetChampionshipDTO> Championships { get; set; }

        public int ChampionshipsCount => this.Championships.Count();
    }
}
