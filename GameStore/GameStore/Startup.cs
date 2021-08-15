
using GameStore.Controllers;
using GameStore.Data.Models;
using GameStore.Infrastructure;
using GameStore.Services.Articles;
using GameStore.Services.Carts;
using GameStore.Services.Comments;
using GameStore.Services.Emails;
using GameStore.Services.Games;
using GameStore.Services.Orders;
using GameStore.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;

namespace GameStore
{
   
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration; 
        }
            

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, Services.Orders.OrderService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);


            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();

                    endpoints.MapControllerRoute(
                        name: "Games Details",
                        pattern: "/Games/Details/{id}/{information}",
                        defaults: new
                        {
                            controller = typeof(GamesController).GetControllerName(),
                            action = nameof(GamesController.Details)
                        });

                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
