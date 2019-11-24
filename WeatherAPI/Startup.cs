using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services;
using WeatherAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Data;
using WeatherAPI.Middleware;
using HotChocolate;
using HotChocolate.AspNetCore;

namespace WeatherAPI
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" });

                c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Description = "basic authentication for API",
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ITemperatureService, TemperatureService>();
            services.AddScoped<IUserService, UserService>();

            services.AddDbContext<WeatherAPIContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WeatherDb")));

            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddQueryType<Query>()
                .Create());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<AddCorrelationIdToResponseMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API V1");

            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseGraphQL("/graphql");
        }
    }
}
