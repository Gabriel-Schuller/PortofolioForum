using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class UserModel
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
