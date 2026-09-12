namespace LearningManagementSystem.Modules.CourseOffering.Domain.ValueObjects;

public sealed record EnrollmentWindow
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public EnrollmentWindow(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date.");

        StartDate = startDate;
        EndDate = endDate;
    }

    
    public bool IsOpen(DateTime currentDate) =>
        currentDate >= StartDate && currentDate <= EndDate;
}

