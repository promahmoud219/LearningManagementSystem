using LearningManagementSystem.Modules.Enrollment.Application.Services;
using LearningManagementSystem.Modules.Enrollment.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Modules.Enrollment.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddEnrollmentInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LMS_Database")
            ?? throw new InvalidOperationException("Connection string 'LMS_Database' is not configured.");

        services.AddScoped<IEnrollmentRepository>(_ => new SqlEnrollmentRepository(connectionString));
        services.AddScoped<IEnrollmentUniquenessChecker>(_ => new EnrollmentUniquenessChecker(connectionString));

        return services;
    }
}