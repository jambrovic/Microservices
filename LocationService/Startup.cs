using LocationService.Models;
using LocationService.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocationService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            
        }

        public IConfigurationRoot Configuration {get;}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}