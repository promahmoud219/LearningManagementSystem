using LearningManagementSystem.Modules.CourseOffering.Application;
using LearningManagementSystem.Modules.CourseOffering.Infrastructure;

using LearningManagementSystem.Modules.Enrollment.Application;
using LearningManagementSystem.Modules.Enrollment.Infrastructure;

using LearningManagementSystem.Modules.Student.Application;
using LearningManagementSystem.Modules.Student.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services
    .AddEnrollmentApplication()
    .AddEnrollmentInfrastructure(builder.Configuration);

builder.Services
    .AddStudentApplication()
    .AddStudentInfrastructure(builder.Configuration);

builder.Services
    .AddCourseOfferingApplication()
    .AddCourseOfferingInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();