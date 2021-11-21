namespace SoccerAPI.Database.Models.Teams
{
    using System;
    using System.Collections.Generic;

    using SoccerAPI.Database.Models.BaseModels;

    public class Coach : Person
    {
        public Coach()
        {

        }

        public string CoachType { get; set; }

        public virtual Team Team { get; set; }
    }
}
