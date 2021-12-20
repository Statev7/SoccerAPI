using System;

namespace SoccerAPI.DTOs.Coach
{
    public class GetCoachDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age => DateTime.UtcNow.Year - this.DateOfBirth.Year;

        public decimal Salary { get; set; }

        public string CoachType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
