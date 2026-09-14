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
        var connectionString = configuration.GetConnectionString("LMS_Database")
            ?? throw new InvalidOperationException("Connection string 'LMS_Database' is not configured.");

        services.AddScoped<ICourseOfferingRepository>(
            _ => new SqlCourseOfferingRepository(connectionString));

        return services;
    }
}
