namespace SoccerAPI.DTOs.Championship
{
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;


    public class PutChampionshipDTO
    {
        [Required]
        [MinLength(ChampionshipConstants.NAME_MIN_LENGHT)]
        [MaxLength(ChampionshipConstants.NAME_MAX_LENGHT)]
        public string Name { get; set; }
    }
}
