namespace SoccerAPI.DTOs.Footballer
{
    using System;

    public class GetFootballerDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age => DateTime.UtcNow.Year - this.DateOfBirth.Year;

        public decimal Salary { get; set; }

        public string Position { get; set; }

        public string StrongLeg { get; set; }

        public int TeamsCount { get; set; }
    }
}
