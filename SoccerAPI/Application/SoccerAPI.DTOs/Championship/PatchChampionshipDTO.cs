namespace SoccerAPI.DTOs.Championship
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;

    public class PatchChampionshipDTO
    {
        [MinLength(ChampionshipConstants.NAME_MIN_LENGHT)]
        [MaxLength(ChampionshipConstants.NAME_MAX_LENGHT)]
        public string Name { get; set; }

        public IEnumerable<Guid> TeamsId { get; set; }
    }
}
