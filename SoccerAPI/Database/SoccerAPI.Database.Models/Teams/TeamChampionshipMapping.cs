namespace SoccerAPI.Database.Models.Teams
{
    using System;

    using SoccerAPI.Database.Models.BaseModels;

    public class TeamChampionshipMapping : BaseModel
    {
        public TeamChampionshipMapping()
            :base()
        {

        }

        public Guid TeamId { get; set; }

        public virtual Team Team { get; set; }

        public Guid ChampionshipId { get; set; }

        public virtual Championship Championship { get; set; }
    }
}
