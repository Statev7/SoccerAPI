namespace SoccerAPI.DTOs.Footballer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;

    public class PutFootballerDTO
    {
        [Required]
        [MinLength(EmployeeConstants.FIRST_NAME_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.FIRST_NAME_MAX_LENGHT)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(EmployeeConstants.SECOND_NAME_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.SECOND_NAME_MAX_LENGHT)]
        public string SecondName { get; set; }

        [Required]
        [MinLength(EmployeeConstants.NATIONALITY_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.NATIONALITY_MAX_LENGHT)]
        public string Nationality { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(typeof(decimal), EmployeeConstants.SALARY_MIN_VALUE, EmployeeConstants.SALARY_MAX_VALUE)]
        public decimal Salary { get; set; }

        [Required]
        [MinLength(FootballerConstants.POSITION_MIN_LENGHT)]
        [MaxLength(FootballerConstants.POSITION_MAX_LENGHT)]
        public string Position { get; set; }

        [Required]
        [MaxLength(FootballerConstants.STRONG_LEG_MAX_VALUE)]
        public string StrongLeg { get; set; }
    }
}
