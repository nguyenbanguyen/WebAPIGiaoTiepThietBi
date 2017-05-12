using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestJWTIn.NetCoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TestJWTIn.NetCoreApi.Options;

namespace TestJWTIn.NetCoreApi
{
    public class Startup
    {
        private const string SecretKey = "ThisStringToMakeUniqueKeyIsn'tIt?";
        private readonly SymmetricSecurityKey _SignInKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(SecretKey));
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
            //Config swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = " API Helper Page",
                    Description = "A simple start ASP.NET Core Web API/ MBAAS",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Nguyễn Bá Nguyên", Email = "", Url = "https://github.com/nguyenbanguyen/" },
                    License = new License { Name = "Under Construction...", Url = " " }
                });
            });
            // Use  policy Auth
            services.AddAuthorization(options =>
            {
                options.AddPolicy("guest", policy => policy.RequireClaim("DisneyCharacter", "IAmMickey"));
                options.AddPolicy("superman", policy => policy.RequireClaim("Role", "Superman"));
            });

            // Add dbcontext with connectionstring from appsettings.json
            var ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PeopleDbContext>(options => 
                 options.UseSqlServer(ConnectionString));

            //add identitycore vào project
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PeopleDbContext>()
                .AddDefaultTokenProviders();
            // Get options from app settings( Appsettings.json), cần add multi appsettings.json dựa vào enviroment
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_SignInKey, SecurityAlgorithms.HmacSha256);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // redirect to https
            //var options = new RewriteOptions().AddRedirectToHttps();
            // sử dụng JWT
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _SignInKey,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });


            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                // Khai báo sử dụng exceptionhander, cần code  /home/error sau
                app.UseExceptionHandler("/Error");
            }
            //sử dụng identity đã khai báo server ở trên
            app.UseIdentity();
            // sử dụng MVC routes
            app.UseMvc(routes =>
            {
                // route for error page handler
                routes.MapRoute(name: "Error", template: "Error",defaults: new { controller = "Error", action = "Error" });
                // SwaggerGen won't find controllers that are routed via this technique.
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            // khởi tạo base data để test
            SeedData.Seed(app);
            // add default account
            SeedData.EnsurePopulated(app);
            // sử dụng swagger
            app.UseSwagger();

            // sử dụng swagger page
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test JWT API V1");
            });
        }
    }
}
