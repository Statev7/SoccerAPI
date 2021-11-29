namespace SoccerAPI.DTOs.Footballer
{
    using System.Collections.Generic;

    public class GetAllFootballersDTO
    {
        public ICollection<GetFootballerDTO> Footballers { get; set; }

        public int FootballersCount => this.Footballers.Count;
    }
}
