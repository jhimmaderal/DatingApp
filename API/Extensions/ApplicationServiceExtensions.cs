using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); 
            });// SQLITE CONNECTION STRING

            services.AddCors();     //API SERVICES 
            services.AddScoped<ITokenService, TokenService>(); //TOKEN SERVICES
            services.AddScoped<IUserRepository, UserRepository>(); // USER REPOSITRY
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // AUTOMAPPER
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings")); // CLOUDINARY KEYS
            services.AddScoped<iPhotoService, PhotoService>(); // CLOUDINARY UPLOADS
            return services;
        }
    }
}