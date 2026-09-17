using LearningManagementSystem.Modules.CourseOffering.Application.Queries.GetAllCourseOfferings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/course-offerings")]
public sealed class CourseOfferingController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllCourseOfferingsQuery(),
            cancellationToken);

        return Ok(result);
    }
}