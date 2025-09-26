using Microsoft.EntityFrameworkCore;
using PrescriptionApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database configuration - FIXED FOR AZURE
builder.Services.AddDbContext<PrescriptionContext>(options =>
{
    // Use SQLite for development, but ensure it works on Azure
    options.UseSqlite(builder.Configuration.GetConnectionString("PrescriptionContext"));
});

// Add routing options
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

// ADD THIS FOR AZURE - Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PrescriptionContext>();
        context.Database.EnsureCreated(); // This will create the database if it doesn't exist
    }
    catch (Exception ex)
    {
        // Log the error (in production, use proper logging)
        Console.WriteLine($"Database creation failed: {ex.Message}");
    }
}

app.Run();