namespace SoccerAPI.DTOs.Footballer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;

    public class PatchFootballerDTO
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

        [MinLength(FootballerConstants.POSITION_MIN_LENGHT)]
        [MaxLength(FootballerConstants.POSITION_MAX_LENGHT)]
        public string Position { get; set; }

        [MaxLength(FootballerConstants.STRONG_LEG_MAX_VALUE)]
        public string StrongLeg { get; set; }
    }
}
