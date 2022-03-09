using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Data.Entities
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(400, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Message { get; set; }
        public int Votes { get; set; } = 0;

        public DateTime Date { get; set; } = DateTime.Now;
        public ICollection<Answer> Answers { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }

    }
}
