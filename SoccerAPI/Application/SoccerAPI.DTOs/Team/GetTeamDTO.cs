namespace SoccerAPI.DTOs.Team
{
    using System;
    using System.Collections.Generic;

    using SoccerAPI.DTOs.Championship;
    using SoccerAPI.DTOs.Coach;
    using SoccerAPI.DTOs.Footballer;

    public class GetTeamDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string Stadium { get; set; }

        public string Country { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public GetAllFootballersDTO Footballers { get; set; }

        public GetAllChampionshipsDTO Championships { get; set; }

        public GetAllCoachesDTO Coaches { get; set; }
    }
}
