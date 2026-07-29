using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // يمكنك تعديل إعدادات كلمة المرور، القفل، إلخ

                //options. Password. RequiredUniqueChars = 2;
                //options. Password. RequireNonAlphanumeric = true;
                //options.Password. RequireUppercase = true;
                //options.Password. RequireLowercase = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/ options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//"Bearer"
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Configure Aunthentication Handler
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"]))

                };

            });
            //.AddJwtBearer("Beaer02", options =>
            //{ 
            
            //})
            //.AddCookie("XXX", options =>
            //{

            //});

            return services;
        }
    }
}
