namespace SoccerAPI.DTOs.Team
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;

    public class PatchTeamDTO
    {
        [MinLength(TeamConstants.NAME_MIN_LENGHT)]
        [MaxLength(TeamConstants.NAME_MAX_LENGHT)]
        public string Name { get; set; }

        [MinLength(TeamConstants.NICKNAME_MIN_LENGHT)]
        [MaxLength(TeamConstants.NICKNAME_MAX_LENGHT)]
        public string Nickname { get; set; }

        [MinLength(TeamConstants.STADIUM_MIN_LENGHT)]
        [MaxLength(TeamConstants.STADIUM_MAX_LENGHT)]
        public string Stadium { get; set; }

        [MaxLength(TeamConstants.COUNTRY_MAX_LENGHT)]
        public string Country { get; set; }

        public IEnumerable<Guid> FootballersId { get; set; }
    }
}
