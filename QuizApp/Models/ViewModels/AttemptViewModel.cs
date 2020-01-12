using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    public class AttemptViewModel
    {
        public Attempt attempt { get; set; }
        public int correct { get; set; }
        public int total { get; set; }
    }
}
