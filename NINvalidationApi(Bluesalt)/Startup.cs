using Bluesalt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Nin.Application;
using Nin.Application.Common.Interface;
using Nin.Application.Common.SmileIdentity.Interface;
using Nin.Infrastructure;
using Nin.Infrastructure.Services.BluesaltService;
using Nin.Infrastructure.Services.SmileIdentityService;
using Nin.Shared.LogService;

namespace NINvalidationApi_Bluesalt_
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
            services.AddScoped<IBluesaltInterface, NinValidationContext>();

            services.AddDbContext<NinValidationContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddScoped<IBluesaltServiceInterface, NinValidationService>();
            services.AddScoped<EnhancedKycVerificationInterface, EnhancedKycVerificationService>();

            services.AddHttpClient();
            services.AddScoped<ILogWritter, LogWriter>();
            services.AddApplication();
            services.AddInfrastructure();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BluesaltApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BluesaltApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
