using System.Text;
using FinanzautoShool.Aplication.Services.Auth.Interface;
using FinanzautoShool.Aplication.Services.Auth.Service;
using FinanzautoShool.Aplication.Services.CourseService.Interface;
using FinanzautoShool.Aplication.Services.CourseService.Service;
using FinanzautoShool.Aplication.Services.QualificationService.Interface;
using FinanzautoShool.Aplication.Services.QualificationService.Service;
using FinanzautoShool.Aplication.Services.StudentService.Interface;
using FinanzautoShool.Aplication.Services.StudentService.Service;
using FinanzautoShool.Aplication.Services.TeacherService.Interface;
using FinanzautoShool.Aplication.Services.TeacherService.Service;
using FinanzautoShool.Infrastructure;
using FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.CourseRepositories.Repository;
using FinanzautoShool.Infrastructure.Repositories.QualificationRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.QualificationRepositories.Repository;
using FinanzautoShool.Infrastructure.Repositories.StudentRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.StudentRepositories.Repository;
using FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Interface;
using FinanzautoShool.Infrastructure.Repositories.TeacherRepositories.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar JWT
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });

builder.Services.AddAuthorization();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });

    // 🔥 Configuración correcta de JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Introduce el token en el siguiente formato: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,  // ✅ Corregido
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddDbContext<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConecction"));
});

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IQualificationService, QualificationService>();
builder.Services.AddScoped<IQualificationRepository, QualificationRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
