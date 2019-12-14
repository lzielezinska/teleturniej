using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    public class QuestionAnswersViewModel
    {
        public Question Question { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
