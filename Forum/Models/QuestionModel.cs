using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class QuestionModel
    {
        [Required]
        [StringLength(400, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Message { get; set; }



    }
}
