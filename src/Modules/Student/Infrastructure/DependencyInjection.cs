using LearningManagementSystem.Modules.Student.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Modules.Student.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LMS_Database")
            ?? throw new InvalidOperationException("Connection string 'LMS_Database' is not configured.");

        services.AddScoped<IStudentRepository>(_ => new SqlStudentRepository(connectionString));

        return services;
    }
}
