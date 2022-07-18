using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using ServerApp.Models;
using ServerApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; 
 
namespace ServerApp
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
            services.AddControllersWithViews();
            services.AddRazorPages();

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            services.AddDbContext<IdentityDataContext>(options =>
                 options.UseSqlServer(Configuration["ConnectionStrings:Identity"]));

            services.AddScoped<ConsoleEmailSender>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddEntityFrameworkStores<IdentityDataContext>()
                 .AddDefaultTokenProviders();

            services.Configure<SecurityStampValidatorOptions>(opts => {
                opts.ValidationInterval = System.TimeSpan.FromMinutes(1);
            });
            
            services.AddScoped<TokenUrlEncoderService>();
            services.AddScoped<IdentityEmailService>();
            
            // services.AddAuthentication()
            //    .AddFacebook(opts => {
            //        opts.AppId = Configuration["Facebook:AppId"];
            //        opts.AppSecret = Configuration["Facebook:AppSecret"];
            //    })
            //    .AddGoogle(opts => {
            //        opts.ClientId = Configuration["Google:ClientId"];
            //        opts.ClientSecret = Configuration["Google:ClientSecret"];
            //    })
            //    .AddTwitter(opts => {
            //        opts.ConsumerKey = Configuration["Twitter:ApiKey"];
            //        opts.ConsumerSecret = Configuration["Twitter:ApiSecret"];
            //        opts.RetrieveUserDetails = true;
            //    });
            
            services.ConfigureApplicationCookie(opts =>{
                opts.LoginPath = "/Identity/SignIn";
                opts.LogoutPath = "/Identity/SignOut";
                opts.AccessDeniedPath = "/Identity/Forbidden";
            }); 

            services.AddDistributedSqlServerCache(options => {
                options.ConnectionString = connectionString;
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });

            services.AddSession(options =>{
                options.Cookie.Name = "QuranHub.Session";
                options.IdleTimeout = System.TimeSpan.FromHours(72);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
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
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<DebugMiddleWare>(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name : "angular_fallback",
                    pattern :"{target:regex(read|search|statistics|notes|mindMaps|analysis|login|logout)}",
                    defaults : new {controller = "Home" , action = "Index"});
            });
            app.UseSpa(spa =>
            {
                string strategy = Configuration.GetValue<string>("DevTools:ConnectionStrategy");

                if (strategy == "proxy")
                {
                    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
                }
                else if (strategy == "managed")
                {
                    spa.Options.SourcePath = "../ClientApp";
                    spa.UseAngularCliServer("start");
                }
            });
            SeedData.SeedDatabase(services.GetRequiredService<DataContext>());
            IdentitySeedData.SeedDatabase(services).Wait();
            app.SeedUserStoreForDashboard();


        }
    } 
}
