using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class CommentModel
    {
        [Required]
        [StringLength(400, MinimumLength = 10)]
        public string Message { get; set; }



    }
}
