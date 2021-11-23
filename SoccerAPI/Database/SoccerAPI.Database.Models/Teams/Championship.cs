namespace SoccerAPI.Database.Models.Teams
{
    using System.Collections.Generic;

    using SoccerAPI.Database.Models.BaseModels;

    public class Championship : BaseModel
    {
        public Championship()
            :base()
        {
            this.Teams = new HashSet<TeamChampionshipMapping>();
        }

        public string Name { get; set; }

        public virtual ICollection<TeamChampionshipMapping> Teams { get; set; }
    }
}
