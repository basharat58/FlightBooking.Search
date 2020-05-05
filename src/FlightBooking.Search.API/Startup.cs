using FlightBooking.Search.Core.Elasticsearch;
using FlightBooking.Search.Core.Configuration;
using FlightBooking.Search.Core.Mapping;
using FlightBooking.Search.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;

namespace FlightBooking.Search.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ElasticsearchConfig>(Configuration.GetSection("Elasticsearch"));
            services.AddTransient<IFlightAvailabilityRepository, FlightAvailabilityRepository>();
            services.AddTransient<IHotelAvailabilityRepository, HotelAvailabilityRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IAirlineRepository, AirlineRepository>();
            services.AddTransient<IElasticSearchClient, ElasticSearchClient>();
            services.AddScoped<IMapper, Mapper>();
            var assembly = AppDomain.CurrentDomain.Load("FlightBooking.Search.Core");
            services.AddMediatR(assembly);            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FlightBooking.Search.API",
                    Version = "v1"
                });
                c.ExampleFilters();
                var filePath = Path.Combine(AppContext.BaseDirectory, "FlightBooking.Search.API.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
            services.AddSwaggerGenNewtonsoftSupport();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "FlightBooking.Search.API");                

                if (env.IsProduction())
                {
                    c.SupportedSubmitMethods();
                }
            });

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
            });
        }
    }
}
