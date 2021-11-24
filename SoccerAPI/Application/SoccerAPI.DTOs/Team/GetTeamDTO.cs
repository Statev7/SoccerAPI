namespace SoccerAPI.DTOs.Team
{
    using System;

    public class GetTeamDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string Stadium { get; set; }

        public string Country { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int FootballersCount { get; set; }

        public int ChampionshipsCount { get; set; }

        public int CoachesCount { get; set; }
    }
}
