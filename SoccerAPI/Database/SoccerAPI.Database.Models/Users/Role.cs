namespace SoccerAPI.Database.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;
    using SoccerAPI.Database.Models.BaseModels;

    public class Role : BaseModel
    {
        public Role()
            :base()
        {
            this.Users = new HashSet<UserRoleMapping>();
        }

        [Required]
        [MaxLength(RoleConstants.NAME_MAX_LENGHT)]
        public string Name { get; set; }

        public virtual ICollection<UserRoleMapping> Users { get; set; }
    }
}
