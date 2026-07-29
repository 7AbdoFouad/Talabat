using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlwares;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Add-Migration "OrderModule" -Context StoreContext -Output /Data/Migartions
            //Remove-Migration -Context StoreContext
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the DI container.
            #region Configure Sevices

            builder.Services.AddControllers();
            //    .AddNewtonsoftJson(options => // Register Required Web APIs Services to the DI Container
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});


            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            }); 

            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddCors(options => //Allow Dependency Injection for CORS Origins Service
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });
            });
            #endregion


            var app = builder.Build();

            #region Updatd-Database And Data Seeding
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            // ASK CLR for Creating Object from DbContext Explicitly
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                await _dbContext.Database.MigrateAsync(); // Updatd-Database
                await StoreContextSeed.SeedAsync(_dbContext); // Data Seeding
              
                await _identityDbContext.Database.MigrateAsync(); // Update Database 
                var _userManager = services.GetRequiredService<UserManager<AppUser>>(); // Explicitly
                await AppIdentityDbContextSeed.SeedUsersAsync(_userManager);// Data Seeding
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "an error has been occured during apply the migration");
            }
            #endregion
            // Configure the HTTP request pipeline.
            #region Configure Kestrel Middlewares

            app.UseMiddleware<ExceptionMiddleware>();
            //3 ways to create Middleware 1.Convention Based   2.Factory Based  3.Request Delegate
            //linkedin.com/feed/update/urn:li:activity:7119577708681961472/
            //Request Delegate 
            ///app.Use(async (httpContext, next) =>
            ///{
            ///    try
            ///    {
            ///        await next.Invoke(httpContext);
            ///    }
            ///    catch (Exception ex)
            ///    {
            ///        logger.LogError(ex, ex.Message); // Development
            ///                                         // Log Exception in (Database | Files) // Production
            ///        httpContext.Response.ContentType = "application/json";
            ///        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ///
            ///        var response = builder.Environment.IsDevelopment() ?
            ///            new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
            ///            : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
            ///
            ///        var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            ///
            ///        var json = JsonSerializer.Serialize(response, options);
            ///        await httpContext.Response.WriteAsync(json);
            ///    }
            ///});

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
                //app. UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("MyPolicy");

            app.MapControllers();
            
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion


            app.Run();
        }
    }
}
