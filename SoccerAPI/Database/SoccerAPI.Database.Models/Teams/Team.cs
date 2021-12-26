namespace SoccerAPI.Database.Models.Teams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;
    using SoccerAPI.Database.Models.BaseModels;
    using SoccerAPI.Database.Models.Championships;

    public class Team : BaseModel
    {
        public Team()
            : base()
        {
            this.Coaches = new HashSet<Coach>();
            this.Footballers = new HashSet<TeamFootballerMapping>();
            this.Championships = new HashSet<ChampionshipTeamMapping>();
        }

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

        public virtual ICollection<Coach> Coaches { get; set; }

        public virtual ICollection<TeamFootballerMapping> Footballers { get; set; }

        public virtual ICollection<ChampionshipTeamMapping> Championships { get; set; }
    }
}
