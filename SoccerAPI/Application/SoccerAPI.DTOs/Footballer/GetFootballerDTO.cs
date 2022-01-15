namespace SoccerAPI.DTOs.Footballer
{
    using System;

    using SoccerAPI.Validator;

    public class GetFootballerDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age => Validator.CalculateAge(this.DateOfBirth, DateTime.UtcNow);

        public decimal Salary { get; set; }

        public string Position { get; set; }

        public string StrongLeg { get; set; }

        public int TeamsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
