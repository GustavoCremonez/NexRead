using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexRead.Application.Interfaces;
using NexRead.Application.Services;
using NexRead.Application.Validators;
using NexRead.Domain.Interfaces;
using NexRead.Infra.Context;
using NexRead.Infra.Repositories;

namespace NexRead.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

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
