namespace SoccerAPI.Database.Models.Teams
{
    using System;

    using SoccerAPI.Database.Models.BaseModels;

    public class TeamFootballerMapping : BaseModel
    {
        public TeamFootballerMapping()
            :base()
        {

        }

        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Guid FootballerId { get; set; }
        public virtual Footballer Footballer { get; set; }
    }
}
