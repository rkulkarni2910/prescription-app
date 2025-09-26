using Microsoft.EntityFrameworkCore;
using PrescriptionApp.Models;

var builder = WebApplication.CreateBuilder(args);

// services to the container.
builder.Services.AddControllersWithViews();

// Database configuration
builder.Services.AddDbContext<PrescriptionContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PrescriptionContext")));


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


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PrescriptionContext>();
    
    // create the database and tables if they don't exist
    context.Database.EnsureCreated();
    
   
}

app.Run();