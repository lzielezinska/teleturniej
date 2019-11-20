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
                // Look for any movies.
                if (context.Question.Any())
                {
                    return;   // DB has been seeded
                }

                context.Question.AddRange(
                    new Question
                    {
                        Content = "Example question 1"
                    },

                    new Question
                    {
                        Content = "Example question 2"
                    }
                );
                context.SaveChanges();

                context.Answer.AddRange(
                    new Answer
                    {
                        Content = "Answer 1 for question 1",
                        QuestionID = 1
                    },

                    new Answer
                    {
                        Content = "Answer 2 for question 1",
                        QuestionID = 1
                    },

                    new Answer
                    {
                        Content = "Answer 3 for question 1",
                        QuestionID = 1
                    },

                    new Answer
                    {
                        Content = "Answer 4 for question 1",
                        QuestionID = 1
                    },

                    new Answer
                    {
                        Content = "Answer 1 for question 2",
                        QuestionID = 2
                    },

                    new Answer
                    {
                        Content = "Answer 2 for question 2",
                        QuestionID = 2
                    },

                    new Answer
                    {
                        Content = "Answer 3 for question 2",
                        QuestionID = 2
                    },

                    new Answer
                    {
                        Content = "Answer 4 for question 2",
                        QuestionID = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
