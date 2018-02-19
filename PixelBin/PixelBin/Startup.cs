using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelBin.Data;
using PixelBin.Models;
using PixelBin.Services;

namespace PixelBin
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            
            services.AddTransient<IPixelBinRepository, PixelBinRepository>();
            services.AddScoped<PixelBinDbContext>(g => { return new PixelBinDbContext(Configuration["ConnectionStrings:DefaultConnection"]); });

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //Causes error:Cannnot open database error ???????
            //CreateAdminRole1(services.BuildServiceProvider());
            CreateAdminRole2(services.BuildServiceProvider());


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        //function version 2--works!!!
        private void CreateAdminRole2(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Task<IdentityResult> roleResult;
            string adminemail = "admin.user@adminmail.com";
            string adminpassword = "_AStrongP@ssword!88";
            string rolename = "Admin";

            //Check that there is an Administrator role and create if not
            Task<bool> hasAdminRole = roleManager.RoleExistsAsync(rolename);
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole(rolename));
                roleResult.Wait();
            }

            //Check if the admin user exists and create it if not
            //Add to the Administrator role

            Task<ApplicationUser> testUser = userManager.FindByEmailAsync(adminemail);
            testUser.Wait();

            if (testUser.Result == null)
            {
                ApplicationUser administrator = new ApplicationUser();
                administrator.Email = adminemail;
                administrator.UserName = adminemail;

                Task<IdentityResult> newUser = userManager.CreateAsync(administrator, adminpassword);
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, rolename);
                    newUserRole.Wait();
                }
            }

        }

        //function version 1
        private async Task CreateAdminRole1(IServiceProvider serviceProvider)
        {

            string adminRole = "Admin";
            string adminname = "Administrator";
            string adminemail = "admin.user@adminmail.com";
            string adminpassword = "PixelBinAdministrator77''";

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            IdentityResult roleResult;


            
            var roleExist = await RoleManager.RoleExistsAsync(adminRole);
            //var roleExist = false;
            if (!roleExist)
            {
               roleResult = await RoleManager.CreateAsync(new IdentityRole(adminRole));
            }
            

            //creating a super user who could maintain the web app
            var adminuser = new ApplicationUser
            {
                UserName = adminname,
                Email = adminemail

            };
            string UserPassword = adminpassword;
            var _user = await UserManager.FindByEmailAsync(adminemail);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(adminuser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(adminuser, "Admin");
                }
            }
        }
    }
}
