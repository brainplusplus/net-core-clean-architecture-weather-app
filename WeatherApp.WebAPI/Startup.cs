using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherApp.Application.Extensions;
using WeatherApp.Persistence.Extensions;
using WeatherApp.Persistence.Context;
using WeatherApp.Service.Extensions;
using WeatherApp.WebAPI.Extensions;

namespace WeatherApp.WebAPI
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
            services.ConfigurePersistence(Configuration);
            services.ConfigureService(Configuration);
            services.ConfigureApplication();

            services.ConfigureApiBehavior();
            services.ConfigureCorsPolicy();
            //default
            services.AddControllers();
            //add
            //services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
            var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
            dataContext?.Database.EnsureCreated();

            serviceScope.DataSeedings();
            
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseErrorHandler();
            app.UseCors();

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
