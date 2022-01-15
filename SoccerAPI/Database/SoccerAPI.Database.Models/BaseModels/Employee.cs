namespace SoccerAPI.Database.Models.BaseModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;

    public abstract class Employee : BaseModel
    {
        [Required]
        [MinLength(EmployeeConstants.FIRST_NAME_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.FIRST_NAME_MAX_LENGHT)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(EmployeeConstants.SECOND_NAME_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.SECOND_NAME_MAX_LENGHT)]
        public string SecondName { get; set; }

        public string FullName => $"{this.FirstName} {this.SecondName}";

        [Required]
        [MinLength(EmployeeConstants.NATIONALITY_MIN_LENGHT)]
        [MaxLength(EmployeeConstants.NATIONALITY_MAX_LENGHT)]
        public string Nationality { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public int Age => SoccerAPI.Validator.Validator.CalculateAge(this.DateOfBirth, DateTime.UtcNow);

        [Required]
        [Range(typeof(decimal), EmployeeConstants.SALARY_MIN_VALUE, EmployeeConstants.SALARY_MAX_VALUE)]
        public decimal Salary { get; set; }

    }
}
