using System;
using System.Web;
using Microsoft.AspNetCore.SignalR;
namespace QuizApp
{
    public class GameHub : Hub
    {
        public void SendQuestion(string question)
        {
            //Clients.All.showQuestion(question);
        }
    }
}
