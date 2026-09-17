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
