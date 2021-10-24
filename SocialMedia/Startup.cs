using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SocialMedia.Database;
using SocialMedia.Repository;
using SocialMedia.Services;


namespace SocialMedia
{
    public class Startup
    {
        protected IConfiguration Configuration { get; }
        protected IWebHostEnvironment HostingEnvironment;  // ?

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddControllersAsServices();

            // add dependency injected services/repositories
            services.AddTransient<ProfileService>();
            services.AddTransient<MajorService>();
            services.AddScoped<ProfileRepository>();
            services.AddScoped<MajorRepository>();
            
            // add database context and use a postgres server for database
            services.AddDbContext<SocialMediaDbContext>(options => options.UseNpgsql(
                Configuration.GetConnectionString("SocialMediaDbContext")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SocialMedia", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialMedia v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}