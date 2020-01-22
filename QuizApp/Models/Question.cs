using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Content { get; set; }
        public int QuizID { get; set; }
        public Quiz Quiz { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
