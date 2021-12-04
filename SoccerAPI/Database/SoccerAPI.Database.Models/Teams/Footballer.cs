namespace SoccerAPI.Database.Models.Teams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;
    using SoccerAPI.Database.Models.BaseModels;

    public class Footballer : Employee
    {
        public Footballer()
            :base()
        {
            
        }

        [Required]
        [MinLength(FootballerConstants.POSITION_MIN_LENGHT)]
        [MaxLength(FootballerConstants.POSITION_MAX_LENGHT)]
        public string Position { get; set; }

        [Required]
        [MaxLength(FootballerConstants.STRONG_LEG_MAX_VALUE)]
        public string StrongLeg { get; set; }

        public virtual ICollection<TeamFootballerMapping> Teams { get; set; }
    }
}
