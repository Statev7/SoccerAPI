namespace SoccerAPI.Database.Models.Teams
{
    using System;
    using System.Collections.Generic;

    using SoccerAPI.Database.Models.BaseModels;

    public class Team : BaseModel
    {
        public Team()
            : base()
        {
            this.Coaches = new HashSet<TeamCoachMapping>();
            this.Footballers = new HashSet<TeamFootballerMapping>();
            this.Championships = new HashSet<TeamChampionshipMapping>();
        }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string Stadium { get; set; }

        public string Country { get; set; }

        public virtual ICollection<TeamCoachMapping> Coaches { get; set; }

        public virtual ICollection<TeamFootballerMapping> Footballers { get; set; }

        public virtual ICollection<TeamChampionshipMapping> Championships { get; set; }
    }
}
