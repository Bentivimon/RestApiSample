using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gateway.Middlewares;
using Gateway.Services;
using Gateway.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Swagger;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AddLogger();

            TestLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(f => f.SwaggerDoc("v." + PlatformServices.Default.Application.ApplicationVersion,
                new Info() { Description = "Desc.", Title = "Title", Version = "v." + System.Environment.Version }));
            services.AddSwaggerGen(sg =>
            {
                {
                    sg.AddSecurityDefinition("Bearer",
                        new ApiKeyScheme
                        {
                            In = "header",
                            Description = "Please enter JWT with Bearer into field",
                            Name = "Authorization",
                            Type = "apiKey"
                        });
                    sg.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer", Enumerable.Empty<string>()},
                    });
                }
                ;

                sg.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    "Gateway.xml"));
            });

            services.AddSingleton<IGroupClient, GroupClient>();
            services.AddSingleton<IStudentsClient, StudentsClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(f =>
            {
                //f.RoutePrefix = "doc";
                f.SwaggerEndpoint("/swagger/v." + PlatformServices.Default.Application.ApplicationVersion + "/swagger.json", "Desc");
            });
        }

        private void AddLogger()
        {
            var serilogConfig = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("serilogconfig.json").Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(serilogConfig)
                .CreateLogger();
        }

        private void TestLogger()
        {
            Log.Logger.Error("Test error");
            Log.Logger.Warning("Test warging");
            Log.Logger.Write(LogEventLevel.Verbose, "Test verbose");
            var startupLogger = Log.Logger.ForContext<Startup>();

            startupLogger.Write(LogEventLevel.Warning, "Test warning from startup");
        }
    }
}
