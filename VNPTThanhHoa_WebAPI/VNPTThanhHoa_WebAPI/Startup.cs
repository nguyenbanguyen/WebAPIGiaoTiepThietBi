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
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VNPTThanhHoa_WebAPI.Data;

namespace VNPTThanhHoa_WebAPI
{
    public class Startup
    {
        /// <summary>
        /// setup for MVC
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddLogging();

            // Add  repository - need to do later
            //services.AddSingleton<ITodoRepository, TodoRepository>();
            // enforce ssl 
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            //// add dbcontext vào sử dụng ngay connection string được khai báo tại đây// với app cần bảo mật thì nên lưu connection string tại appsettings.json.
            services.AddDbContext<VNPTAPIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = " API Helper Page",
                    Description = "A simple start ASP.NET Core Web API/ MBAAS",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Nguyễn Bá Nguyên", Email = "", Url = "https://github.com/nguyenbanguyen/" },
                    License = new License { Name = "Under Construction...", Url = " " }
                });
                //Set the comments path for the swagger json and ui.
                // onlyworking on local, need to be fixed
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, ".xml");
                //c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, VNPTAPIContext VnptDbContext)
        {
            
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // redirect to https
            //var options = new RewriteOptions().AddRedirectToHttps();
            // setup mvc

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                // Khai báo sử dụng exceptionhander, cần code  /home/error sau
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            // config swagger
            //setup mvc routes
            app.UseMvc(routes =>
            {
                    routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            //app.UseDefaultFiles();
            //app.UseMvcWithDefaultRoute();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // tạo file swagger json
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            // tạo page swagger helper từ swagger json
            app.UseSwaggerUI(c =>
            {
                //  url / description
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VNPT API V1.0");

            });
            //DbInitializer.Initialize(VnptDbContext);
            

        }
    }
}
