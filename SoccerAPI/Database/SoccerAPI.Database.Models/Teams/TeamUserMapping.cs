namespace SoccerAPI.Database.Models.Teams
{
    using System;

    using SoccerAPI.Database.Models.BaseModels;
    using SoccerAPI.Database.Models.Users;

    public class TeamUserMapping : BaseModel
    {
        public TeamUserMapping()
            :base()
        {

        }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
