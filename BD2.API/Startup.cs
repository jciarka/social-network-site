using BD2.API.Configuration;
using BD2.API.Database;
using BD2.API.Database.Entities;
using BD2.API.Database.Repositories;
using BD2.API.Database.Repositories.Concrete;
using BD2.API.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD2.API
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
            services.AddTransient<IBooksRepository, BooksRepository>();
            services.AddTransient<IAuthorsRepository, AuthorsRepository>(); 

            // services.AddTransient<IRepo, Repo>();
            var tokenConfiguration = new TokenConfiguration { SecurityKey = "qwertyuiopasdfghjklzxcvbnm" };

            var securityKey = Encoding.ASCII.GetBytes(tokenConfiguration.SecurityKey);
            var validationConfiguration = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false
            };

            services.AddSingleton(tokenConfiguration);
            services.AddSingleton(validationConfiguration);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = false;
                jwt.TokenValidationParameters = validationConfiguration;
            });

            services.AddIdentity<User, Role>(
                    options => options.SignIn.RequireConfirmedAccount = true )
                    .AddEntityFrameworkStores<AppDbContext>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("dbConnection")
                ));

            // Enable model validation
            services.AddControllers()
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.SuppressModelStateInvalidFilter = true;
                        // Supress filter in order to avoid validating model before entering - disables auto generate 400 response when DataAnnotations not met
                        // Its also possibble to remove api controller but it is better solution: https://stackoverflow.com/questions/51870603/intercept-bad-requests-before-reaching-controller-in-asp-net-core
                    });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BD2.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BD2.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
