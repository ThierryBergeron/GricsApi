using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleApi.Helpers;
using SimpleApi.Models.Firestore;
using SimpleApi.Models.Repositories;
using SimpleApi.Services;

namespace SimpleApi
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
            services.AddCors(o => o.AddPolicy("Api", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            var tokenSettings = Configuration.GetSection("TokenSettings").Get<TokenSettings>();
            services.AddSingleton(tokenSettings);
            services.AddScoped<IAuthService, AuthService>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.JwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var firestoreCredentials = Configuration.GetSection("FirestoreCredentials").Get<FirestoreCredentials>();
            services.AddSingleton(firestoreCredentials);

            services.AddControllersWithViews();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMockDataGenerator, MockDataGenerator>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IReportCardRepository, ReportCardRepository>();
            services.AddScoped<IQueryServices, QueryServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("Api");

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

                endpoints.MapControllerRoute(
                    name: "auth",
                    pattern: "{controller=Authentication}/{action=Index}/{id?}",
                    new[] { "TranslationApi.Api" }
                );
            });
        }
    }
}
