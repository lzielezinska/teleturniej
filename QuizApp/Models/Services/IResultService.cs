using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models.Services
{
    public interface IResultService
    {
        void CreateResult(Result result);
        ResultViewModel GetResult(int? attempt);
    }
}
