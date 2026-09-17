using LearningManagementSystem.Modules.CourseOffering.Application.Services;
using LearningManagementSystem.Modules.CourseOffering.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Modules.CourseOffering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCourseOfferingApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped<
            ICourseOfferingEnrollmentEligibilityChecker,
            CourseOfferingEnrollmentEligibilityChecker>();

        services.AddScoped<
            ICourseOfferingEnrollmentInfoProvider,
            CourseOfferingEnrollmentInfoProvider>();

        services.AddScoped<
            ICourseOfferingPricing,
            CourseOfferingPricingService>();

        return services;
    }
}