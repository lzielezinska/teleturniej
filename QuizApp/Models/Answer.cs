using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Answer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Content { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
    }
}
