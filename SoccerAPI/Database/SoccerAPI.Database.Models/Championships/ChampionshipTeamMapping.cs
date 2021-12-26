namespace SoccerAPI.Database.Models.Championships
{
    using System;

    using SoccerAPI.Database.Models.BaseModels;
    using SoccerAPI.Database.Models.Teams;

    public class ChampionshipTeamMapping : BaseModel
    {
        public ChampionshipTeamMapping()
            :base()
        {

        }

        public Guid TeamId { get; set; }

        public virtual Team Team { get; set; }

        public Guid ChampionshipId { get; set; }

        public virtual Championship Championship { get; set; }
    }
}
