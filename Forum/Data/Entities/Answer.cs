using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Data.Entities
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(400, MinimumLength = 10)]
        public string Message { get; set; }
        public int Votes { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

        public int? QuestionId { get; set; }

        public Question Question { get; set; }

    }
}
