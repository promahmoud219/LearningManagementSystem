using MediatR;

using LearningManagementSystem.Modules.Enrollment.Application.Contracts;
using LearningManagementSystem.Modules.Enrollment.Application.Services;

using LearningManagementSystem.Modules.Enrollment.Domain.Aggregates;
using LearningManagementSystem.Modules.Enrollment.Domain.Enums;
using LearningManagementSystem.Modules.Enrollment.Domain.Repositories;
using LearningManagementSystem.Modules.Enrollment.Domain.ValueObjects;

using LearningManagementSystem.Modules.CourseOffering.Application.Contracts;

using LearningManagementSystem.SharedKernel.Results;
using LearningManagementSystem.SharedKernel.ValueObjects;

namespace LearningManagementSystem.Modules.Enrollment.Application.Commands.RequestEnrollment;                                                                 

internal sealed class RequestEnrollmentCommandHandler
    : IRequestHandler<RequestEnrollmentCommand, OperationResult<EnrollmentId>>
{
    private readonly EnrollmentEligibilityService _enrollmentEligibilityService;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseOfferingPricing _pricing;


    public RequestEnrollmentCommandHandler(
        EnrollmentEligibilityService eligibilityService,
        ICourseOfferingPricing pricing,
        IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentEligibilityService = eligibilityService;
        _enrollmentRepository = enrollmentRepository;
        _pricing = pricing;
    }

    public async Task<OperationResult<EnrollmentId>> Handle(
        RequestEnrollmentCommand request,
        CancellationToken cancellationToken)
    {
        var eligibility = await _enrollmentEligibilityService.CheckEligibilityAsync(
            request.StudentId, request.CourseOfferingId, cancellationToken);

        if (eligibility != EligibilityResult.Eligible)
        {
            return OperationResult<EnrollmentId>.ValidationError(
                new Error(eligibility.ToString(), "Student is not eligible to enroll in this course offering."));
        }

        var basePrice = await _pricing.GetCurrentPriceAsync(request.CourseOfferingId, cancellationToken);

        var discount = Money.Zero(basePrice.Currency); //  discount code, resolution 

        var enrollment = Enrollment.Create(
            EnrollmentId.Create(),
            request.StudentId,
            request.CourseOfferingId,
            DateTime.UtcNow,
            basePrice,
            discount,
            request.DiscountCode);

        await _enrollmentRepository.AddAsync(enrollment, cancellationToken);

        // TODO : SaveChanges + publish enrollment.DomainEvents with MediatR

        return OperationResult<EnrollmentId>.Success(enrollment.Id);
    }
}