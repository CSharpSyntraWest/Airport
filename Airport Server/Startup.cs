using Airport_DAL.Context;
using Airport_DAL.Services;
using Airport_Server.Converter;
using Airport_Server.Hubs;
using Airport_Server.Services;
using Airport_Server.Services.DALServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Airport_Server
{
    public class Startup
    {
        private IUpdateClientService UpdateClientService;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IServerService, ServerService>(serviceProvider =>
            {
                return new ServerService
                (
                    serviceProvider.GetRequiredService<IConverterProvider>(),
                    serviceProvider.GetRequiredService<IDALService>(),
                    AirportLoader.Load
                );
            });

            services.AddSingleton<IAirportDataService, AirportDataService>();
            services.AddSingleton<IConverterProvider, ConverterProvider>();
            services.AddSingleton<UpdateClientService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IDALService, DALService>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.UpdateClientService = app.ApplicationServices.GetService<IUpdateClientService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AirportHub>("/airport");
            });
        }
    }
}
