using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class RegisterUserModel
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string UserName { get; set; }
        
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Email { get; set; }
    }
}
