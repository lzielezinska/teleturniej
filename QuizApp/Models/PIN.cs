using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    public class PIN
    {
        public int Id { get; set; }
        public int QuizID { get; set; }
        public Quiz Quiz { get; set; }
        public String UserId { get; set; }
        public IdentityUser User { get; set; }
        [Required]
        [StringLength(6)]
        public string Code { get; set; }
        public Boolean Active { get; set; }
    }
}
