namespace SoccerAPI.DTOs.Coach
{
    using System.Collections.Generic;

    public class GetAllCoachesDTO
    {
        public ICollection<GetCoachDTO> Coaches { get; set; }
    }
}
