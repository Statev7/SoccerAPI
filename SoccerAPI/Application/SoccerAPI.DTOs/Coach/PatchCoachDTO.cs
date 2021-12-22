namespace SoccerAPI.DTOs.Coach
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;

    public class PatchCoachDTO
    {
        [MinLength(EmployeeConstants.FIRST_NAME_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.FIRST_NAME_MAX_LENGHT)]
        public string FirstName { get; set; }

        [MinLength(EmployeeConstants.SECOND_NAME_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.SECOND_NAME_MAX_LENGHT)]
        public string SecondName { get; set; }

        [MinLength(EmployeeConstants.NATIONALITY_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.NATIONALITY_MAX_LENGHT)]
        public string Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Range(typeof(decimal), EmployeeConstants.SALARY_MIN_VALUE, EmployeeConstants.SALARY_MAX_VALUE)]
        public decimal Salary { get; set; }

        public string CoachType { get; set; }
    }
}
