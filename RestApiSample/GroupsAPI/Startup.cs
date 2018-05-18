using System.Collections.Generic;
using System.IO;
using System.Linq;
using GroupsAPI.Data.Context;
using GroupsAPI.Data.Seeder;
using GroupsAPI.Services.Abstractions;
using GroupsAPI.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace GroupsAPI
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
            services.AddDbContext<DatabaseContext>(options =>
              options.UseMySql(Configuration.GetConnectionString("LocalConnection")), ServiceLifetime.Transient);

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
                    "GroupsAPI.xml"));
            });

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            SeedDatabase(app);

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


        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<GroupSeeder>();
        }


        private void SeedDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var defaultSeeder = serviceScope.ServiceProvider.GetService<GroupSeeder>();

                defaultSeeder.Seed();
            }
        }
    }
}
