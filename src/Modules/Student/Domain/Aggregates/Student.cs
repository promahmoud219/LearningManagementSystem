using System.Runtime.InteropServices;

using LearningManagementSystem.SharedKernel.ValueObjects;
using LearningManagementSystem.Student.Domain.ValueObjects;

namespace LearningManagementSystem.Modules.Student.Domain.Aggregates;

public sealed class Student (
    StudentId id,
    Name firstName,
    Name lastName,
    Email email,
    List<EnrolledCourses> enrolledCourses,
    ) : AggregateRoot
{
    public StudentId Id { get; } = id;
    public FullName FirstName { get; } = firstName;
    public FullName LastName { get; } = lastName;
    public Email Email { get; } = email;
    public List<CourseId> EnrolledCourses { get; } = enrolledCourses;

    private readonly List<CourseId> _completedCourses = new();
    public IReadOnlyCollection<CourseId> CompletedCourses => _completedCourses.AsReadOnly();

    public static Student Create(
        StudentId id, 
        Name firstName, 
        Name lastName, 
        Email email) 
        => new(id, firstName, lastName, email);


    public void MarkCourseAsCompleted(CourseId courseId)
    {
        if (!_completedCourses.Contains(courseId))
        {
            _completedCourses.Add(courseId);
        }
    }




    public StudentEnrollmentEligibilityResult CheckEnrollmentEligibility(CourseOffering courseOffering)
    {
        foreach (var prerequisite in courseOffering.Prerequisites)
        {
            if (!CompletedCourses.Any(c => c.CourseId == prerequisite))
            {
                return StudentEnrollmentEligibilityResult.PrerequisitesNotMet;
            }
        }
        return StudentEnrollmentEligibilityResult.Eligible;
    }
}