using LearningManagementSystem.Modules.CourseOffering.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Modules.CourseOffering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCourseOfferingInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LMS")
            ?? throw new InvalidOperationException("Connection string 'LmsDatabase' is not configured.");

        services.AddScoped<ICourseOfferingRepository>(
            _ => new SqlCourseOfferingRepository(connectionString));

        return services;
    }
}
