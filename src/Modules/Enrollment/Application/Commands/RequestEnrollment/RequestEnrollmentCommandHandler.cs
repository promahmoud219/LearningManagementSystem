using MediatR;
 
using LearningManagementSystem.Modules.Enrollment.Application.Services;

using LearningManagementSystem.Modules.Enrollment.Domain.Aggregates;
using LearningManagementSystem.Modules.Enrollment.Domain.Enums;
using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;
using LearningManagementSystem.Modules.Enrollment.Application.Repositories;

using LearningManagementSystem.Modules.CourseOffering.Contracts;

using LearningManagementSystem.SharedKernel.Results;
using LearningManagementSystem.SharedKernel.ValueObjects;
using EnrollmentAggregate = LearningManagementSystem.Modules.Enrollment.Domain.Aggregates.Enrollment;

namespace LearningManagementSystem.Modules.Enrollment.Application.Commands.RequestEnrollment;

internal sealed class RequestEnrollmentCommandHandler
    (EnrollmentEligibilityService enrollmentEligibilityService,
     IEnrollmentRepository enrollmentRepository,
     ICourseOfferingPricing pricing)
    : IRequestHandler<RequestEnrollmentCommand, OperationResult<EnrollmentId>>
{
    private readonly EnrollmentEligibilityService _enrollmentEligibilityService = enrollmentEligibilityService;
    private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
    private readonly ICourseOfferingPricing _pricing = pricing;


    public async Task<OperationResult<EnrollmentId>> Handle(
        RequestEnrollmentCommand request,
        CancellationToken cancellationToken)
    {
        var eligibility = await _enrollmentEligibilityService.IsEligibleAsync(
            request.StudentId, request.CourseOfferingId, cancellationToken);

        if (eligibility != EligibilityResult.Eligible)
        {
            return OperationResult<EnrollmentId>.ValidationError(
                new Error(eligibility.ToString(), "Student is not eligible to enroll in this course offering."));
        }

        var basePrice = await _pricing.GetCurrentPriceAsync(request.CourseOfferingId, cancellationToken);

        var discount = Money.Zero(basePrice.Currency); //  discount code, resolution 

        var enrollment = EnrollmentAggregate.Create(
            request.StudentId,
            request.CourseOfferingId,
            DateTime.UtcNow,
            basePrice,
            discount,
            request.DiscountCode);

        var enrollmentId = await _enrollmentRepository.AddAsync(enrollment, cancellationToken);

        return OperationResult<EnrollmentId>.Success(enrollmentId);
    }
}
