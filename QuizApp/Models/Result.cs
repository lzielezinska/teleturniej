using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
        public int AttemptID { get; set; }
        public Attempt Attempt { get; set; }
        public Boolean Response { get; set; }

    }
}
