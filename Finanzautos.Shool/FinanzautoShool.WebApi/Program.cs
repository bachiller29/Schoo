using FinanzautoShool.Aplication.Services.CourseService.Interface;
using FinanzautoShool.Aplication.Services.CourseService.Service;
using FinanzautoShool.Aplication.Services.StudentService.Interface;
using FinanzautoShool.Aplication.Services.StudentService.Service;
using FinanzautoShool.Aplication.Services.TeacherService.Interface;
using FinanzautoShool.Aplication.Services.TeacherService.Service;
using FinanzautoShool.Infrastructure;
using FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Repository;
using FinanzautoShool.Infrastructure.Repositories.StudentRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.StudentRepositories.Repository;
using FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConecction"));
});

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
