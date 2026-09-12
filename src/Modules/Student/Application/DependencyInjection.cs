using LearningManagementSystem.Modules.Student.Application.Services;
using LearningManagementSystem.Modules.Student.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Modules.Student.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentApplication(this IServiceCollection services)
    {
        services.AddScoped<IStudentEnrollmentEligibilityChecker, StudentEnrollmentEligibilityChecker>();
        services.AddScoped<IStudentEnrollmentInfoProvider, StudentEnrollmentInfoProvider>();

        return services;
    }
}
