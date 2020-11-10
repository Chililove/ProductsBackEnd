using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using ProductsProject.Core.ApplicationService;
using ProductsProject.Core.ApplicationService.Service;
using ProductsProject.Core.DomainService;
using ProductsProject.Infrastructure.Data;
using ProductsProject.Infrastructure.Data.Helper;
using ProductsProject.Infrastructure.Data.Repositories;

namespace ProductsWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;

        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                  
                    ValidateIssuer = false,
                   
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.FromMinutes(5) 
                };
            });
            //rvices.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

            var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

            services.AddScoped<IUserRepository<User>, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
           // services.AddScoped<IDBInitializer, DBInitializer>();
            services.AddTransient<IDBInitializer, DBInitializer>();
            services.AddSingleton<IAuthenticationHelper>(new
               Authenticationhelper(secretBytes));
            services.AddSwaggerGen();
            services.AddMvc().AddNewtonsoftJson();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers().AddNewtonsoftJson(options =>
            {    // Use the default property (Pascal) casing
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.MaxDepth = 2;  // 100 limit per owner
            });

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<Context>(opt => { opt.UseSqlite("Data Source=ProductDb.db"); }
                );
            }
           /* else
            {
                services.AddDbContext<Context>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }*/

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CustomerAppAllowSpecificOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .WithMethods();
                    });
            });

            services.AddControllers();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var ctx = scope.ServiceProvider.GetService<Context>();
                //ctx.Database.EnsureCreated();
                var dbInitializer = services.GetService<IDBInitializer>();
                dbInitializer.Initialize(ctx);
            }



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var ctx = services.GetService<Context>();
                    var dbInit = services.GetService<IDBInitializer>();
                    dbInit.Initialize(ctx);
                }
            }
            else
            {
                app.UseHsts();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var ctx = services.GetService<Context>();
                    ctx.Database.EnsureCreated();

                }

            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CustomerAppAllowSpecificOrigins");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
