using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now.Date;
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
