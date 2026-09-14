using FluentValidation;
using LearningManagementSystem.Modules.Enrollment.Application.Services;
using LearningManagementSystem.Modules.Enrollment.Application.Commands.RequestEnrollment;
using LearningManagementSystem.SharedKernel.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystem.Modules.Enrollment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddEnrollmentApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                typeof(RequestEnrollmentCommandHandler).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<RequestEnrollmentCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<EnrollmentEligibilityService>();

        return services;
    }
}   