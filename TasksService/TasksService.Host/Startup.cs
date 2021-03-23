using JetBrains.Annotations;
using LightInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TasksService.Db;

namespace TasksService.Host
{
    public class Startup
    {
        private IServiceContainer ServiceContainer { get; set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // Use this method to add services directly to LightInject
        // Important: This method must exist in order to replace the default provider.
        [UsedImplicitly]
        public void ConfigureContainer(IServiceContainer container)
        {
            ServiceContainer = container;
            container.RegisterInstance(Configuration);
            container.RegisterFrom<ContainerCompositionRoot>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            EntityToDbTypeMappingConfiguration.InitConfig();

            // services.AddCors(o => o.AddPolicy("MyPolicy",
            //     builder =>
            //     {
            //         builder.AllowAnyOrigin()
            //             .AllowAnyMethod()
            //             .AllowAnyHeader();
            //     }));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}