using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexRead.Application.AppServices;
using NexRead.Application.Services;
using NexRead.Domain.Repositories;
using NexRead.Infra.Context;
using NexRead.Infra.Repositories;
using NexRead.Infra.Services;

namespace NexRead.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddScoped<IAuthAppService, AuthAppService>();
        services.AddScoped<IUserAppService, UserAppService>();
        
        services.AddValidatorsFromAssembly(typeof(CreateAuthorValidator).Assembly);

        #region Author
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        #endregion

        #region General
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        #endregion

        return services;
    }
}
