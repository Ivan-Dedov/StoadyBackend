using System;
using System.IO;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stoady.Database.Repositories;
using Stoady.Helpers;

namespace Stoady
{
    public class Startup
    {
        public const string VersionName = "0.100";
        public const string ApplicationName = "Stoady";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint($"/swagger/{VersionName}/swagger.json", ApplicationName));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response =
                    new
                    {
                        error = exception.Message
                    };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public static void ConfigureServices(
            IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(VersionName,
                    new OpenApiInfo
                    {
                        Title = ApplicationName,
                        Version = VersionName
                    });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthorizationOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthorizationOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthorizationOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            // todo? services.AddLogging(builder => builder.AddConsole());

            services.AddMediatR(typeof(Startup));
            services.AddValidatorsFromAssemblies(new [] {Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
