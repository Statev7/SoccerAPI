namespace SoccerAPI.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    using SoccerAPI.Common.Constants.ModelConstants;

    public class PostUserRegisterDTO
    {
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
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}
