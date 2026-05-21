using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eduRateSystem.Data;
using eduRateSystem.Models;
using eduRateSystem.Seed;

var builder = WebApplication.CreateBuilder(args);

var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (!string.IsNullOrEmpty(rawConnectionString))
{
    var masked = rawConnectionString;

    var passwordIndex = masked.IndexOf("Password=", StringComparison.OrdinalIgnoreCase);
    if (passwordIndex >= 0)
    {
        var end = masked.IndexOf(';', passwordIndex);
        if (end >= 0)
            masked = masked.Substring(0, passwordIndex) + "Password=***" + masked.Substring(end);
        else
            masked = masked.Substring(0, passwordIndex) + "Password=***";
    }

    Console.WriteLine("ACTIVE CONNECTION STRING:");
    Console.WriteLine(masked);
}
else
{
    Console.WriteLine("ACTIVE CONNECTION STRING: NULL");
}

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",
            "https://steady-douhua-9753e7.netlify.app"
        )
              .AllowCredentials()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("FrontendPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await IdentitySeeder.SeedRolesAsync(roleManager);
        await IdentitySeeder.SeedAdminUserAsync(userManager, roleManager);
    }
}
catch (Exception ex)
{
    Console.WriteLine("STARTUP ERROR:");
    Console.WriteLine(ex.ToString());
    throw;
}

app.Run();
