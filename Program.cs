using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using cineVote.Models.Domain;
using cineVote.Repositories.Implementation;
using cineVote.Repositories.Abstract;


namespace cineVote
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //DbContext configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<Person, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/UserAuthentication/Login";
            });

            
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IUserAuthService, UserAuthService>();
            builder.Services.AddScoped<ICompetitionManager, CompetitionManager>();
            builder.Services.AddScoped<IAdminTasks, AdminTasks>();
            builder.Services.AddScoped<ITMDBApiService, TMDBApiService>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}