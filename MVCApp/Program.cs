using MVCApp;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddAzureAppConfiguration(
    options => { 
        options.Connect("Endpoint=https://myappconfiguration08022024.azconfig.io;Id=aX37;Secret=A05e600tRwpaWKkdFDuY0epbEt9WKJyKvGh2h6x5nM6vY0qwarYDJQQJ99AHACi5YpzNU05oAAACAZAC0fdH");
        //options.UseFeatureFlags();
    });




// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<EmployeeRepository>(); // Register the repository

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
