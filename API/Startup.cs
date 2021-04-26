using System.CodeDom.Compiler;
using System.Threading.Tasks;
using BLL.Facades;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repository;
using GIL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ObjectContracts;
using Scrutor;

namespace Project_Chronos_Backend
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

            services.Configure<ConnectionStrings>(option =>
                Configuration.GetSection("ConnectionStrings").Bind(option));
            ScanForAllRemainingRegistrations(services);
        }
        public static void ScanForAllRemainingRegistrations(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Startup), typeof(ProjectFacade), typeof(BaseRepository), typeof(Git))
                .AddClasses(x => x.WithoutAttribute(typeof(GeneratedCodeAttribute)))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            //To stop the hosted service crying register services as transient by default, not scoped. This API is stateless so shouldn't cause issues
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
