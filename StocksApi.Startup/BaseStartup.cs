using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

using Serilog;

using StocksApi.Core;
using StocksApi.Data;
using StocksApi.Extensions;
using StocksApi.Service.Companies;
using StocksApi.Service.EndOfDayData;

namespace StocksApi.Controllers
{
    public abstract class BaseStartup
    {
        protected BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            services.AddSingleton(Log.Logger);
            services.AddSingleton<IEndOfDayUpdate, EndOfDayUpdate>();
            services.AddSingleton<ICompanyInformation, CompanyInformation>();
            services.AddSingleton<ICompanyInformationStore, AsxCompanyInformationStore>();
            services.AddSingleton<IEndOfDayUpdate, EndOfDayUpdate>();
            services.AddSingleton<IEndOfDayStore, FileEndOfDayStore>();

            var origin = Configuration.GetValueOrEnvironmentVariable(Constants.StocksApiClientApplicationOrigin);

            // CORS config for Azure is done in the portal, not in code
            if (!String.IsNullOrEmpty(origin))
            {
                services.AddCors(
                    options =>
                    {
                        options.AddDefaultPolicy(
                            builder =>
                            {
                                builder.WithOrigins(origin);
                                builder.AllowAnyMethod();
                                builder.AllowAnyHeader();
                            });
                    });
            }

            services.AddOData();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;

                foreach (var outputFormatter in options.OutputFormatters.OfType<OutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<InputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            var connectionString = Configuration.GetConnectionString(Constants.StocksApiStocksDbConnectionString);

            if (String.IsNullOrEmpty(connectionString))
                connectionString = Environment.GetEnvironmentVariable(Constants.StocksApiStocksDbConnectionString);

            if (Environment.GetEnvironmentVariable(Constants.AspNetCoreEnvironment) == Constants.AspNetCoreEnvironmentDevelopment)
            {
                services.AddDbContext<StocksContext>(o =>
                    {
                        o.UseSqlite(connectionString);

                        if (Constants.TrueOrOne.Contains(Environment.GetEnvironmentVariable(Constants.StocksApiSensitiveLogging)?.ToLower()))
                            o.EnableSensitiveDataLogging();
                    }
                );
            }
            else
            {
                services.AddDbContext<StocksContext>(o =>
                    {
                        o.UseSqlServer(connectionString);

                        if (Constants.TrueOrOne.Contains(Environment.GetEnvironmentVariable(Constants.StocksApiSensitiveLogging)?.ToLower()))
                            o.EnableSensitiveDataLogging();
                    }
                );
            }

            services.AddAutoMapper(mc => mc.AddProfile(new StockProfile()));
        }

        protected virtual void ConfigureAdditionalServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            BeforeConfigureUseRouting(app, env);

            app.UseRouting();

            if (env.IsDevelopment())
                app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.Select().Filter().OrderBy().Count().MaxTop(10);
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        protected virtual void BeforeConfigureUseRouting(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}

////using Microsoft.AspNetCore.Builder;
////using Microsoft.AspNetCore.Hosting;
////using Microsoft.Extensions.Configuration;
////using Microsoft.Extensions.DependencyInjection;
////using Microsoft.Extensions.Hosting;

////namespace Stocks
////{
////    public class Startup
////    {
////        public Startup(IConfiguration configuration)
////        {
////            Configuration = configuration;
////        }

////        public IConfiguration Configuration { get; }

////        // This method gets called by the runtime. Use this method to add services to the container.
////        public void ConfigureServices(IServiceCollection services)
////        {
////            //services.AddRazorPages();
////            services.AddSpaStaticFiles(s => s.RootPath = "wwwroot");
////            services.AddControllers();

////            //services.AddAuthentication(options =>
////            //    {
////            //        options.DefaultScheme = "cookie";
////            //        options.DefaultChallengeScheme = "oidc";
////            //    })
////            //    .AddCookie("cookie")
////            //    .AddOpenIdConnect("oidc", options =>
////            //    {
////            //        options.Authority = "https://localhost:5000";
////            //        options.ClientId = "oidcClient";
////            //        options.ClientSecret = "SuperSecretPassword";
    
////            //        options.ResponseType = "code";
////            //        options.UsePkce = true;
////            //        options.ResponseMode = "query";
    
////            //        // options.CallbackPath = "/signin-oidc"; // default redirect URI
        
////            //        // options.Scope.Add("oidc"); // default scope
////            //        // options.Scope.Add("profile"); // default scope
////            //        options.Scope.Add("api1.read");
////            //        options.SaveTokens = true;
////            //    });


////        }

////        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
////        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
////        {
////            if (env.IsDevelopment())
////            {
////                app.UseDeveloperExceptionPage();
////            }
////            else
////            {
////                app.UseExceptionHandler("/Error");
////                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
////                app.UseHsts();
////            }

////            app.UseHttpsRedirection();
////            app.UseDefaultFiles();
////            app.UseSpaStaticFiles();

////            app.UseRouting();

////            app.UseAuthorization();

////            app.UseEndpoints(endpoints =>
////            {
////                endpoints.MapControllers();
////            });
////        }
////    }
////}
