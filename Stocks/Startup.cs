using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Stocks
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
            //services.AddRazorPages();
            services.AddSpaStaticFiles(s => s.RootPath = "wwwroot");
            services.AddControllers();

            //services.AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = "cookie";
            //        options.DefaultChallengeScheme = "oidc";
            //    })
            //    .AddCookie("cookie")
            //    .AddOpenIdConnect("oidc", options =>
            //    {
            //        options.Authority = "https://localhost:5000";
            //        options.ClientId = "oidcClient";
            //        options.ClientSecret = "SuperSecretPassword";
    
            //        options.ResponseType = "code";
            //        options.UsePkce = true;
            //        options.ResponseMode = "query";
    
            //        // options.CallbackPath = "/signin-oidc"; // default redirect URI
        
            //        // options.Scope.Add("oidc"); // default scope
            //        // options.Scope.Add("profile"); // default scope
            //        options.Scope.Add("api1.read");
            //        options.SaveTokens = true;
            //    });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
