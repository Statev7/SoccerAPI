namespace SoccerAPI.DTOs.User
{
    using System.ComponentModel.DataAnnotations;

    public class PostUserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
