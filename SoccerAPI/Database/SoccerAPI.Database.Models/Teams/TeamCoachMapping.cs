namespace SoccerAPI.Database.Models.Teams
{
    using System;

    using SoccerAPI.Database.Models.BaseModels;

    public class TeamCoachMapping : BaseModel
    {
        public TeamCoachMapping()
            : base()
        {

        }

        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Guid CoachId { get; set; }
        public virtual Coach Coach { get; set; }
    }
}
