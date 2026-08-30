using FluentValidation;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Application.Commands.RequestEnrollment;

public sealed class RequestEnrollmentCommandValidator : AbstractValidator<RequestEnrollmentCommand>
{
    public RequestEnrollmentCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEqual(StudentId.Empty)
            .WithMessage("Student id is required.");

        RuleFor(x => x.CourseOfferingId)
            .NotEqual(CourseOfferingId.Empty)
            .WithMessage("Course offering id is required.");

        RuleFor(x => x.DiscountCode)
            .MaximumLength(50)
            .When(x => x.DiscountCode is not null)
            .WithMessage("Discount code is too long.");
    }
}