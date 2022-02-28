using System;
using System.IO;
using System.Reflection;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Stoady.DataAccess.DataContexts;

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

            // Контекст БД Stoady
            services.AddDbContext<StoadyDataContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    assembly => assembly.MigrationsAssembly("Stoady.DataAccess.Migrations"))
            );

            // Dependency Injection
            services.AddMediatR(typeof(Startup));
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
