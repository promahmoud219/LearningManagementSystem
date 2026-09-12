using LearningManagementSystem.Modules.Enrollment.Application;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEnrollmentApplication();


builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services
    .AddEnrollmentApplication()
    .AddEnrollmentInfrastructure(builder.Configuration);

builder.Services
    .AddStudentApplication()
    .AddStudentInfrastructure(builder.Configuration);

builder.Services
    .AddCourseOfferingApplication()
    .AddCourseOfferingInfrastructure(builder.Configuration);

builder.Services
    .AddPaymentApplication()
    .AddPaymentInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();