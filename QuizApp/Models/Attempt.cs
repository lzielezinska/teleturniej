using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace QuizApp.Models
{
    public class Attempt
    {
        public int Id { get; set; }
        public int QuizID { get; set; }
        public Quiz Quiz { get; set; }
        public String UserId { get; set; }
        public IdentityUser User { get; set; }
        public DateTime StartDate { get; set; }
        public List<Result> Results { get; set; }
    }
}
