using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using QuizApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using QuizApp.Services;
using QuizApp.Models.Services;
//using Owin;
//using Microsoft.Owin;
//[assembly: OwinStartup(typeof(QuizApp.Startup))]
namespace QuizApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<QuizDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("QuizAppContext")));

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<QuizDbContext>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuizService, QuizService>();
            services.AddTransient<IAttemptService, AttemptService>();
            services.AddTransient<IResultService, ResultService>();
            services.AddTransient<IPINService, PINService>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddSignalR();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //    name: "quiz-question",
                //    pattern: "{controller=Quiz}/{quizId}/{action=Question}/{numberOfQuestion}");
                endpoints.MapRazorPages();
            });
        }
    }
}
