namespace SoccerAPI.Database.Models.Teams
{
    using System;
    using System.Collections.Generic;

    using SoccerAPI.Database.Models.BaseModels;

    public class Coach : Employee
    {
        public Coach()
            :base()
        {

        }

        public string CoachType { get; set; }

        public virtual Team Team { get; set; }
    }
}
