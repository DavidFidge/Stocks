using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StocksApi.Controllers;

namespace StocksApi
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureAdditionalServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(s => s.RootPath = "wwwroot");
        }

        protected override void BeforeConfigureUseRouting(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseSpaStaticFiles();
        }
    }
}
