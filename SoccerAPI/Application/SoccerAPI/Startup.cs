namespace SoccerAPI
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    using SoccerAPI.Common;
    using SoccerAPI.Database;
    using SoccerAPI.Infrastructure.Filters;
    using SoccerAPI.Infrastructure.Middlewares;
    using SoccerAPI.Services.Database;
    using SoccerAPI.Services.Database.Contracts;

    using static SoccerAPI.Database.Seed.Launcher;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoccerAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = $"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.OperationFilter<AuthResponsesOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddDbContext<SoccerAPIDbContext>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            this.AddDatabaseServices(services);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoccerAPI v1"));
                MigrateDatabase(app).GetAwaiter().GetResult();
                SeedDatabaseAsync(app).GetAwaiter().GetResult();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void AddDatabaseServices(IServiceCollection services)
        {
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IFootballerService, FootballerService>();
            services.AddScoped<ITeamFootballerMappingService, TeamFootballerMappingService>();
            services.AddScoped<IChampionshipService, ChampionshipService>();
            services.AddScoped<ITeamChampionshipMappingService, TeamChampionshipMappingService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<ITeamCoachService, TeamCoachService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleMappingService, UserRoleMappingService>();
        }

        private static async Task MigrateDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider;
                var dbContext = service.GetRequiredService<SoccerAPIDbContext>();

                using (dbContext)
                {
                   await dbContext.Database.MigrateAsync();
                }
            }
        }
    }
}
