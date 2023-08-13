using MailKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyStudentApi.Data;
using MyStudentApi.Repository;
using MyStudentApi.Repository.IRepo;
using NETCore.MailKit.Core;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TendancyDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAttendanceRepo, AttendanceRepo>();
builder.Services.AddScoped<IStudent, StudentRepo>();
builder.Services.AddScoped<ILecturesRepo, LecturesRepo>();
builder.Services.AddTransient<IEMailServices, MailServices>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));



builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));






builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Attendance", Version = "v1" });
 
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAttendance");
});
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
