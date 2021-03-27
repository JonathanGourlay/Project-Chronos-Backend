using System.Threading.Tasks;
using ProjectChronosBackend.DAL;
using ProjectChronosBackend.DAL.Interfaces;
using ProjectChronosBackend.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Project_Chronos_Backend.DAL.Interfaces;
using Project_Chronos_Backend.DAL.Repository;

namespace ProjectChronosBackend
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

            services.AddCors(options =>
            {
                options.AddPolicy("CORS", builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.UseOneOfForPolymorphism();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AD API",
                    Version = "1.0",
                    Description =
                        "An api for my Diss.",
                    Contact = new OpenApiContact
                    {
                        Name = "Jonathan",
                        Email = "Jonathan@theroad.uk"
                    },
                });

                

            });

            services.AddSingleton<IProjectRepo, ProjectRepo>();
            services.AddSingleton<IUserRepo, UserRepo>();
            services.Configure<ConnectionStrings>(option =>
                Configuration.GetSection("ConnectionStrings").Bind(option));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseCors("CORS");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Porject Chronos Backend");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.FromResult(0);
                });
                endpoints.MapControllers();
            });
        }
    }
}
