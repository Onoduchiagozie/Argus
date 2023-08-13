using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Repository;
using MyStudentApi.Repository.IRepo;
using MyStudentAttendance.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
 builder.Services.AddTransient<ILecturesRepo, LecturesRepo>();
builder.Services.AddTransient<IStudent,StudentRepo>();
 
builder.Services.AddDbContext<TendancyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<WeatherForecastService>();

 var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
