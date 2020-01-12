using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    public class QuestionAnswersViewModel
    {
        public Question Question { get; set; }
        public Boolean IsAnswerFinal { get; set; }
        public int? AttemptId { get; set; }
    }
}
