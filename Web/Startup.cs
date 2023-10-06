using Log;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Logging;
using Web.Filters;

namespace Web
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
            services.AddHealthChecks()
            .AddCheck("meu-health-check", () => HealthCheckResult.Healthy("Componente está disponível"));

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            IdentityModelEventSource.ShowPII = true;
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44316/") // Substitua pelo seu domínio
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 100000000;
            });

            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2CAPI", options));
            
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Evita a formatação das propriedades
            });

            services.AddControllers();
            services.AddHttpContextAccessor();           
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Erro");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Add the Microsoft Identity Web cookie policy
            app.UseCookiePolicy();
            app.UseRouting();
            // Add the ASP.NET Core authentication service
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigin");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}",
                defaults: new { controller = "Home", action = "Index" });

            app.MapControllerRoute(
                name: "RotaAPI",
                pattern: "api/{controller=Home}/{action=Index}/{id?}");

            //Altera as claims do usuário em tempo de execução 
            app.UseMiddleware<CustomIdentityValidation>();                      

            app.UseEndpoints(endpoints =>
            {
                // Configuração do endpoint para os health checks
                endpoints.MapHealthChecks("/health");
            });

            var logProv = new LogConfigProvider(Configuration);
            var logConfig = logProv.GetLogConfig();
            LogArquivo.Initialize(logConfig);

            app.Run();
        }

    }
}

