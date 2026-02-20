using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Infrastructure.Repositories;

namespace PulseDesk.Infrastructure.Extension;
public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PulseDeskDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Default")));

        services.AddScoped<IIncidentRepository, IncidentRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        return services;
    }
}
