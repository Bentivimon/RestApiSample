using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Gateway
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(f =>
            {
                //f.RoutePrefix = "doc";
                f.SwaggerEndpoint("/swagger/v." + PlatformServices.Default.Application.ApplicationVersion + "/swagger.json", "Desc");
            });
        }
    }
}
