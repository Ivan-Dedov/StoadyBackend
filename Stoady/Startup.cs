using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

using FluentValidation;
using FluentValidation.AspNetCore;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Stoady.DataAccess.Repositories;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Helpers;
using Stoady.Services;
using Stoady.Services.Interfaces;

namespace Stoady
{
    public class Startup
    {
        public const string ApplicationName = "Stoady";
        private const string ApplicationVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

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

            // TODO: JWT авторизация
            // var key = Encoding.ASCII.GetBytes(AuthorizationOptions.Secret);
            // services.AddAuthentication(x =>
            //     {
            //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     })
            //     .AddJwtBearer(x =>
            //     {
            //         x.RequireHttpsMetadata = true;
            //         x.SaveToken = true;
            //         x.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(key),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //     });

            // Dependency Injection
            services.AddMediatR(typeof(Startup));

            // Валидация
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            // Обработчик исключений
            services.AddTransient<ExceptionHandlingMiddleware>();

            // Services
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IClaimService, ClaimService>();
            services.AddTransient<IPasswordValidatorService, PasswordValidatorService>();
            services.AddScoped<IRightsValidatorService, RightsValidatorService>();

            // Repositories
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<ITopicRepository, TopicRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint($"/swagger/{ApplicationVersion}/swagger.json", ApplicationName));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Красиво кидаем исключения
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
