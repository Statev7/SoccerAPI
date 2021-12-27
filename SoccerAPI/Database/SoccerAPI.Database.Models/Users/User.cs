namespace SoccerAPI.Database.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;
    using SoccerAPI.Database.Models.BaseModels;
    using SoccerAPI.Database.Models.Teams;

    public class User : BaseModel
    {
        public User()
            :base()
        {
            this.Roles = new HashSet<UserRoleMapping>();
            this.Teams = new HashSet<TeamUserMapping>();
        }

        [Required]
        [MinLength(UserConstants.FIRST_NAME_MIN_LENGHT)]
        [MaxLength(UserConstants.FIRST_NAME_MAX_LENGHT)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(UserConstants.LAST_NAME_MIN_LENGHT)]
        [MaxLength(UserConstants.LAST_NAME_MAX_LENGHT)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Salt { get; set; }

        public virtual ICollection<UserRoleMapping> Roles { get; set; }

        public virtual ICollection<TeamUserMapping> Teams { get; set; }
    }
}
