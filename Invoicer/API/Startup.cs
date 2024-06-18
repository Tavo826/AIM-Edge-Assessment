using Application.Implementations;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Persistence.Adapter;
using Infrastructure.Persistence.Mappers;
using Infrastructure.Persistence.Repositories;
using MySql.Data.MySqlClient;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<InvoiceRepository, InvoiceRepositoryImpl>();
            services.AddScoped<UserRepository, UserRepositoryImpl>();
            services.AddScoped<IServiceGeneric, ServiceGeneric>();
            services.AddScoped<InvoiceAdapter, InvoiceAdapterImpl>();
            services.AddScoped<UserAdapter, UserAdapterImpl>();

            services.Configure<Settings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton(new MySqlConnection(Configuration.GetSection("AppSettings:ServiceConfig:ConnectionString").Value));

            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors(options => options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }));

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Invoicer API",
                    Description = "Invoice detail visualizer"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/version", async context =>
                {
                    await context.Response.WriteAsync(Environment.GetEnvironmentVariable("VERSION") ?? "0.0.0");
                });
                endpoints.MapControllers();
            });

            app.UseSwagger(c => c.SerializeAsV2 = true);
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
        }
    }
}
