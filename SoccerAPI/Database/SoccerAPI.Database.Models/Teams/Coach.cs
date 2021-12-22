namespace SoccerAPI.Database.Models.Teams
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;
    using SoccerAPI.Database.Models.BaseModels;

    public class Coach : Employee
    {
        public Coach()
            : base()
        {

        }

        [Required]
        [MinLength(CoachConstants.MIN_COACH_TYPE_LENGHT)]
        [MaxLength(CoachConstants.MAX_COACH_TYPE_LENGHT)]
        public string CoachType { get; set; }

        public Guid? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
