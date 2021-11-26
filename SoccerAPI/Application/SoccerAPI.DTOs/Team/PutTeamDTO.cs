namespace SoccerAPI.DTOs.Team
{
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;

    public class PutTeamDTO
    {
        [Required]
        [MinLength(TeamConstants.NAME_MIN_LENGHT)]
        [MaxLength(TeamConstants.NAME_MAX_LENGHT)]
        public string Name { get; set; }

        [Required]
        [MinLength(TeamConstants.NICKNAME_MIN_LENGHT)]
        [MaxLength(TeamConstants.NICKNAME_MAX_LENGHT)]
        public string Nickname { get; set; }

        [Required]
        [MinLength(TeamConstants.STADIUM_MIN_LENGHT)]
        [MaxLength(TeamConstants.STADIUM_MAX_LENGHT)]
        public string Stadium { get; set; }

        [Required]
        [MaxLength(TeamConstants.COUNTRY_MAX_LENGHT)]
        public string Country { get; set; }
    }
}
