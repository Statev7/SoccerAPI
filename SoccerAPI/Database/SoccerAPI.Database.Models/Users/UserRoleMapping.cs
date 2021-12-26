namespace SoccerAPI.Database.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Database.Models.BaseModels;

    public class UserRoleMapping : BaseModel
    {
        public UserRoleMapping()
            :base()
        {

        }

        [Required]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
