using System;
using System.IO;
using System.Reflection;
using System.Text;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Stoady.DataAccess.DataContexts;
using Stoady.Helpers;
using Stoady.Services;
using Stoady.Services.Interfaces;
using Stoady.Validators;
using Stoady.Validators.Interfaces;

namespace Stoady
{
    public class Startup
    {
        private const string ApplicationName = "Stoady";
        private const string ApplicationVersion = "v0.1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApplicationVersion,
                    new OpenApiInfo
                    {
                        Title = ApplicationName,
                        Version = ApplicationVersion
                    });

                // Документация
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // JWT авторизация
            var key = Encoding.ASCII.GetBytes(AuthorizationOptions.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Контекст БД Stoady
            services.AddDbContext<StoadyDataContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    assembly => assembly.MigrationsAssembly("Stoady.DataAccess.Migrations"))
            );

            // Dependency Injection
            services.AddMediatR(typeof(Startup));

            // Services
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IClaimService, ClaimService>();

            // Validators
            services.AddTransient<ITeamValidator, TeamValidator>();
            services.AddTransient<IUserValidator, UserValidator>();
            services.AddTransient<IUserTeamValidator, UserTeamValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint($"/swagger/{ApplicationVersion}/swagger.json", ApplicationName));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Делаем отображение ошибок как JSON
            app.UseExceptionHandler(
                c => c.Run(
                    async context =>
                    {
                        var exception = context.Features
                            .Get<IExceptionHandlerPathFeature>()
                            ?.Error;

                        var response =
                            new
                            {
                                source = exception?.Source,
                                error = exception?.Message,
                                stackTrace = exception?.StackTrace
                            };

                        await context.Response.WriteAsJsonAsync(response);
                    }));

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
