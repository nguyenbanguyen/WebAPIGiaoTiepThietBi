using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace VNPTThanhHoa_WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        /// <summary>
        /// Set up for using controller
        /// </summary>
        
        public IConfigurationRoot Configuration { get; }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // config swagger helping page below
            app.UseMvcWithDefaultRoute();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            // Link của Json swagger sử dụng để tạo API Helper Page
            app.UseSwaggerUI(c =>
            {
                // URL của json và Description
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VNPT API V1");
            });
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            // config swagger helping page below

            services.AddLogging();

            // Add our repository type-- cần config
            //services.AddSingleton<ITodoRepository, TodoRepository>();
            //services.AddSingleton<>
            // Register the Swagger generator, defining one or more Swagger documents
            //config title và version hiển thị tại trang /swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "VNpt API",
                    Description = "A simple start ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Nguyễn Bá Nguyên", Email = "", Url = "https://github.com/nguyenbanguyen" },
                    License = new License { Name = "To Be Determined", Url = "http://url.com" }
                });
                // thiết lập vị trí  xml documented file sử dụng cho swagger
                // cần thiết lập để có thể chạy trên host clound
                //var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, ".xml");
                //c.IncludeXmlComments(filePath);
                //c.IncludeXmlComments( , "MyApi.xml");
                //c.IncludeXmlComments(string.Format(@"{0}\App_Data\MyApi.XML",  System.AppDomain.CurrentDomain.BaseDirectory));
                //var filePath = Path.Combine("App_Data/netcoreapp1.1", ".xml");
                //c.IncludeXmlComments(filePath);
            });
        }


    }
}
