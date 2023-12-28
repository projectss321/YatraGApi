using Microsoft.AspNetCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
using yatracub.Models;
using yatracub.Repository;
using yatracub.Repository.Interface;
using yatracub.Services;
using yatracub.Services.Interface;
using yatracub.Shared;
using yatracub.Shared.Interface;

namespace yatracub
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseKestrel(opt => {
                var sp = opt.ApplicationServices;
                using (var scope = sp.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<dbContext>();
                    //var e = dbContext.Certificates.FirstOrDefault();
                    // now you get the certificates
                }
            });
    
    public void ConfigureServices(IServiceCollection services)
        {
            //add all service(Interface) and repository
            AppSettings appsettings = new AppSettings();
            MailSettings mailSettings = new MailSettings();
            configRoot.GetSection("AppSettings").Bind(appsettings);
            configRoot.GetSection("MailSettings").Bind(mailSettings);
            services.AddSingleton(appsettings);
            services.AddSingleton(mailSettings);
            services.AddTransient<IdbContext, dbContext>();
            services.AddTransient<Iuserservice, userservice>();
            services.AddTransient<Iuserrepo, userrepo>();
            services.AddTransient<IbookingHistoryservice, bookingHisoryService>();
            services.AddTransient<IbookingHistoryrepo, bookinghistoryrepo>();
            services.AddTransient<IcommonFunction, commonFunction>();
            services.AddTransient<IStateCityService, stateCityService>();
            services.AddTransient<IstateCityRepo, stateCityRepo>();
            services.AddTransient<IApplicationTypeServices, ApplicationTypeServices>();
            services.AddTransient<IEmailSend, EmailSend>();
            services.AddTransient<IEmailSendRepo, EmailSendRepo>();
            services.AddTransient<Ipackageservice, packageService>();
            services.AddTransient<IpackageRepo, packagerepo>();
            services.AddHttpContextAccessor();

            //enable cors
            services.AddCors(options =>
            {
                options.AddPolicy("ApiCorsPolicy", builder =>
                {
                    builder.SetIsOriginAllowed(o => true)
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200/", "http://www.localhost:4200/")
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
                    .WithOrigins("http://localhost:4200", "https://localhost:4200/")
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
                    .WithOrigins("http://localhost:4200/Home", "http://www.localhost:4200/Home")
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS");
                });
            });
            
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            //app.UseRouting();
            //app.UseAuthorization();
            //app.MapRazorPages();
            //app.Run();

            app.UseCors("ApiCorsPolicy");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
