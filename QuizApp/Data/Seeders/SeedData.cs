using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Models;
using System;
using System.Linq;

namespace QuizApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new QuizDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<QuizDbContext>>()))
            {
                // Look for any questions.
                if (context.Question.Any())
                {
                    return;   // DB has been seeded
                }

                context.Quiz.AddRange(
                    new Quiz { Name = "Quiz no. 1" }
                );
                context.SaveChanges();

                context.Question.AddRange(
                    new Question { Content = "My husband ……………… the car every Saturday.", QuizID = 1 },
                    new Question { Content = "You ……………………… your socks on the floor, I just can’t stand it!", QuizID = 1 },
                    new Question { Content = "\"Whose bag is this?\" \"Oh, it ……… to me. I’ll take it if it is blocking the way.\"", QuizID = 1 },
                    new Question { Content = "I’ve always dreamt ………………… Australia.", QuizID = 1 },
                    new Question { Content = "That’s the first time someone ………………… something so rude to me!", QuizID = 1 },
                    new Question { Content = "…………………………… films on Netflix for the last six hours? Turn it off and get some exercise!", QuizID = 1 },
                    new Question { Content = "I’m afraid Mr Smith has …………………… left the office. Do you want to leave him a message?", QuizID = 1 },
                    new Question { Content = "I’m tired of this. I …………………………… of leaving this job for good.", QuizID = 1 },
                    new Question { Content = "How much ……………………… to get to this club?", QuizID = 1 },
                    new Question { Content = "I’ve got a good chance of getting this position, ……… I do fine during the interview.", QuizID = 1 }
                );
                context.SaveChanges();

                context.Answer.AddRange(
                    new Answer { Content = "is washing", QuestionID = 1 },
                    new Answer { Content = "wash", QuestionID = 1 },
                    new Answer { Content = " washes", QuestionID = 1, IsCorrect = true },
                    new Answer { Content = "is wash", QuestionID = 1 },
                    new Answer { Content = "are always leaving", QuestionID = 2, IsCorrect = true },
                    new Answer { Content = "was always leaving", QuestionID = 2 },
                    new Answer { Content = "always leave", QuestionID = 2 },
                    new Answer { Content = "have always been left", QuestionID = 2 },
                    new Answer { Content = "is belonging", QuestionID = 3 },
                    new Answer { Content = "has belonged", QuestionID = 3 },
                    new Answer { Content = "belongs", QuestionID = 3, IsCorrect = true },
                    new Answer { Content = "is belonged", QuestionID = 3 },
                    new Answer { Content = "to visit", QuestionID = 4},
                    new Answer { Content = "of visiting", QuestionID = 4, IsCorrect = true },
                    new Answer { Content = "of to visit", QuestionID = 4},
                    new Answer { Content = "to visiting", QuestionID = 4},
                    new Answer { Content = "has said", QuestionID = 5, IsCorrect = true },
                    new Answer { Content = "is saying", QuestionID = 5},
                    new Answer { Content = "says", QuestionID = 5 },
                    new Answer { Content = "say", QuestionID = 5 },
                    new Answer { Content = "Do you watch", QuestionID = 6},
                    new Answer { Content = "Are you watching", QuestionID = 6 },
                    new Answer { Content = "You watch", QuestionID = 6 },
                    new Answer { Content = "Have you been watching", QuestionID = 6, IsCorrect = true },
                    new Answer { Content = "just", QuestionID = 7, IsCorrect = true },
                    new Answer { Content = "still", QuestionID = 7 },
                    new Answer { Content = "now", QuestionID = 7 },
                    new Answer { Content = "yet", QuestionID = 7},
                    new Answer { Content = "think", QuestionID = 8},
                    new Answer { Content = "am thinking", QuestionID = 8, IsCorrect = true },
                    new Answer { Content = "use to think", QuestionID = 8 },
                    new Answer { Content = "have thinking", QuestionID = 8 },
                    new Answer { Content = "it costs", QuestionID = 9 },
                    new Answer { Content = "is it costing", QuestionID = 9 },
                    new Answer { Content = "does it costs", QuestionID = 9, IsCorrect = true },
                    new Answer { Content = "costs", QuestionID = 9 },
                    new Answer { Content = "unless", QuestionID = 10 },
                    new Answer { Content = "only", QuestionID = 10 },
                    new Answer { Content = "while", QuestionID = 10},
                    new Answer { Content = "provided", QuestionID = 10, IsCorrect = true }
                );
                context.SaveChanges();
            }
        }
    }
}
