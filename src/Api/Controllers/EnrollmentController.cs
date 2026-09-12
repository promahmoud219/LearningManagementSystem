using LearningManagementSystem.Modules.Enrollment.Application.Commands.RequestEnrollment;


using Microsoft.AspNetCore.Mvc;
using MediatR;


[ApiController]
[Route("api/enrollments")]
public sealed class EnrollmentController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> RequestEnrollment(
        RequestEnrollmentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }
}


/*
[ApiController]
[Route("api/enrollments")]
public sealed class EnrollmentController : ControllerBase
{
    private readonly ISender _sender;

    public EnrollmentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> RequestEnrollment(
        RequestEnrollmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RequestEnrollmentCommand(
            request.StudentId,
            request.CourseOfferingId);

        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{enrollmentId:guid}")]
    public async Task<IActionResult> GetEnrollment(
        Guid enrollmentId,
        CancellationToken cancellationToken)
    {
        var query = new GetEnrollmentQuery(enrollmentId);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("~/api/students/{studentId:guid}/enrollments")]
    public async Task<IActionResult> GetStudentEnrollments(
        Guid studentId,
        CancellationToken cancellationToken)
    {
        var query = new GetStudentEnrollmentsQuery(studentId);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost("{enrollmentId:guid}/withdraw")]
    public async Task<IActionResult> WithdrawEnrollment(
        Guid enrollmentId,
        CancellationToken cancellationToken)
    {
        var command = new WithdrawEnrollmentCommand(enrollmentId);

        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }
}

 */