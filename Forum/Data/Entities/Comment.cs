using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Data.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(400, MinimumLength = 10)]
        public string Message { get; set; }
        public int Votes { get; set; } = 0;
        public int? UserId { get; set; }

        public User User { get; set; }

        public int? AnswerId { get; set; }
        public Answer Answer { get; set; }


    }
}
