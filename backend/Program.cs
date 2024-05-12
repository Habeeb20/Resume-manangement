using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using backend.Core.AutoMapperConfig;
using backend.DataContext;


var builder = WebApplication.CreateBuilder(args);

//DB configuration

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("local"));
});

//Automapper Configuration

builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfile));

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();


// // Add services to the container.
// builder.Services.AddControllersWithViews();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(options => 
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
