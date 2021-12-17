using System.ComponentModel.DataAnnotations;

using SoccerAPI.Common.Constants.ModelConstants;

namespace SoccerAPI.DTOs.Championship
{
    public class PostChampionshipDTO
    {
        [Required]
        [MinLength(ChampionshipConstants.NAME_MIN_LENGHT)]
        [MaxLength(ChampionshipConstants.NAME_MAX_LENGHT)]
        public string Name { get; set; }
    }
}
