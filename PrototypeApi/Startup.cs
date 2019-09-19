using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace PrototypeApi
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

            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = serviceProvider.GetService<ApiContext>();
            AddTestData(context);

            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }

        private void AddTestData(ApiContext context)
        {
            var testUser1 = new DbModels.User
            {
                Id = "abc123",
                FirstName = "Luke",
                LastName = "Skywalker"
            };
            var testUser2 = new DbModels.User
            {
                Id = "tat654",
                FirstName = "Obi-Van",
                LastName = "Kenobi"
            };
            var testUser3 = new DbModels.User
            {
                Id = "lkj654",
                FirstName = "Jabba",
                LastName = "Griffin"
            };
            context.Users.AddRange(testUser1, testUser2, testUser3);
            

            var testPost1 = new DbModels.Post
            {
                Id = "cal789",
                UserId = testUser1.Id,
                Content = "What a piece of junk!"
            };

          
            var testPost2 = new DbModels.Post
            {
                Id = "qwe369",
                UserId = testUser2.Id,
                Content = "Excellent its working"
            };

            var testPost3 = new DbModels.Post
            {
                Id = "ewq852",
                UserId = testUser2.Id,
                Content = "I need a coffe after this"
            };

            var testPost4 = new DbModels.Post
            {
                Id = "asd478",
                UserId = testUser3.Id,
                Content = "What a suprise."
            };

            var testPost5 = new DbModels.Post
            {
                Id = "gfh357",
                UserId = testUser3.Id,
                Content = "Pancake"
            };
            var testPost6 = new DbModels.Post
            {
                Id = "gfh389",
                UserId = testUser3.Id,
                Content = "Pancake"
            };
            context.Posts.AddRange(testPost1, testPost2, testPost3, testPost4, testPost5, testPost6);

            context.SaveChanges();
        }
    }
}
