using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ProductionMonitoring.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterPmServer();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
