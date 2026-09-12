using LearningManagementSystem.Modules.Enrollment.Application.Contracts;
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
        var connectionString = configuration.GetConnectionString("LmsDatabase")
            ?? throw new InvalidOperationException("Connection string 'LmsDatabase' is not configured.");

        services.AddScoped<IEnrollmentRepository>(_ => new SqlEnrollmentRepository(connectionString));
        services.AddScoped<IEnrollmentUniquenessChecker>(
            _ => new EnrollmentUniquenessChecker(connectionString));

        return services;
    }
}
