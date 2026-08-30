using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Modules.Enrollment.Infrastructure; // ???? Enrollment ??? ????????

public static class DependencyInjection
{
    public static IServiceCollection AddEnrollmentInfrastructure(this IServiceCollection services/*, IConfiguration configuration*/)
    {
        // 1. ????? ??? DbContext (????? ????????)
        // services.AddDbContext<EnrollmentDbContext>(options => ...);

        // 2. ????? ??? Repositories
        // services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

        // 3. ????? ??? Checkers/External services
        // services.AddScoped<IEnrollmentUniquenessChecker, EnrollmentUniquenessChecker>();

        return services;
    }
}