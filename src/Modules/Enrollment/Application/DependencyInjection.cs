using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LearningManagementSystem.Modules.Enrollment.Application; // ???? Enrollment ??? ????????

public static class DependencyInjection
{
    public static IServiceCollection AddEnrollmentApplication(this IServiceCollection services)
    {
        // 1. ????? MediatR ????? ???? ??? ??????? ???
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // 2. ????? FluentValidation (?? ????????)
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // 3. ????? ??? Services ?????? ?????????
        // services.AddScoped<IEnrollmentEligibilityService, EnrollmentEligibilityService>();

        return services;
    }
}