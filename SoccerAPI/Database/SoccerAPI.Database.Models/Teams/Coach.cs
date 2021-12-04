namespace SoccerAPI.Database.Models.Teams
{
    using System;

    using SoccerAPI.Database.Models.BaseModels;

    public class Coach : Employee
    {
        public Coach()
            : base()
        {

        }

        public string CoachType { get; set; }

        public Guid TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
