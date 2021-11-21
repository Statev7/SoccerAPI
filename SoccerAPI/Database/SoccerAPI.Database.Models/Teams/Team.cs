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
        }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public DateTime Founded { get; set; }

        public string Stadium { get; set; }

        public string Country { get; set; }

        public virtual ICollection<TeamCoachMapping> Coaches { get; set; }
    }
}
