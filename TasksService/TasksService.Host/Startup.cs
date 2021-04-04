using AuthStuff.Container;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TasksService.Db;
using TasksService.Repositories;
using TasksService.Repositories.Contracts;
using TasksService.Services;
using TasksService.Services.Contracts;
using TasksService.Streams;
using TasksService.Streams.Contracts;

namespace TasksService.Host
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
            EntityToDbTypeMappingConfiguration.InitConfig();

            services.TryAddSingleton(Configuration);
            services.TryAddScoped<IDbProvider, DbProvider>();
            services.TryAddScoped<IUsersRepository, UsersRepository>();
            services.TryAddScoped<IPopugTaskRepository, PopugTaskRepository>();
            services.TryAddScoped<IPopugTaskAdministrationService, PopugTaskAdministrationService>();
            services.TryAddScoped<ITaskBusinessEventsStream, TaskBusinessEventsStream>();

            services.TryAddScopedAuthServices();
            services.AddHostedAuthService();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}