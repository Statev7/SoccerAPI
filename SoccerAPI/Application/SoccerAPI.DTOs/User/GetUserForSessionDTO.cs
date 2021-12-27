namespace SoccerAPI.DTOs.User
{
    using System;
    using System.Collections.Generic;

    using SoccerAPI.Database.Models.Users;

    public class GetUserForSessionDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<GetRoleFroSessionDTO> Roles { get; set; }
    }
}
