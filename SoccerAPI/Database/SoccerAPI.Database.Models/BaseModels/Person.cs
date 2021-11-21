namespace SoccerAPI.Database.Models.BaseModels
{
    using System;

    public abstract class Person : BaseModel
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age => DateTime.UtcNow.Year - this.DateOfBirth.Year;
    }
}
