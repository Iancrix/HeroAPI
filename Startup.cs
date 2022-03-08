using Microsoft.EntityFrameworkCore;
using HeroAPI.Models;
using System.Text.Json.Serialization;

namespace HeroAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });

            services.AddControllers()
                .AddJsonOptions(opt =>
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                ).AddNewtonsoftJson();

            // Use SQL Server DB
            // services.AddDbContext<HeroAcademiaContext>(opt =>
            //     opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")
            // ));

            // Use In-memory DB
            services.AddDbContext<HeroAcademiaContext>(opt => opt.UseInMemoryDatabase("HeroAPIDB"));

            services.AddAutoMapper(typeof(Startup));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowOrigin");

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}