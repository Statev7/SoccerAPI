namespace SoccerAPI.DTOs.Championship
{
    using System;

    public class GetChampionshipDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int TeamsCount { get; set; }
    }
}
