namespace SoccerAPI.Database.Models.Teams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;
    using SoccerAPI.Database.Models.BaseModels;

    public class Championship : BaseModel
    {
        public Championship()
            :base()
        {
            this.Teams = new HashSet<TeamChampionshipMapping>();
        }

        [Required]
        [MinLength(ChampionshipConstants.NAME_MIN_LENGHT)]
        [MaxLength(ChampionshipConstants.NAME_MAX_LENGHT)]
        public string Name { get; set; }

        public virtual ICollection<TeamChampionshipMapping> Teams { get; set; }
    }
}
