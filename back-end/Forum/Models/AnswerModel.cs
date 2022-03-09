using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class AnswerModel
    {
        [Required]
        [StringLength(400, MinimumLength = 10)]
        public string Message { get; set; }

    }
}
