namespace SoccerAPI.DTOs.Team
{
    using System.Collections.Generic;

    public class GetAllTeamsDTO
    {
        public ICollection<GetTeamDTO> Teams { get; set; }

        public int TeamsCount => this.Teams.Count;
    }
}
