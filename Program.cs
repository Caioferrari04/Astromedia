using Astromedia.Models;
using Astromedia.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

string connectionString = "";

if (builder.Environment.IsProduction())
{
    var PGUSER = Environment.GetEnvironmentVariable("PGUSER");
    var PGPASSWORD = Environment.GetEnvironmentVariable("PGPASSWORD");
    var PGHOST = Environment.GetEnvironmentVariable("PGHOST");
    var PGPORT = Environment.GetEnvironmentVariable("PGPORT");
    var PGDATABASE = Environment.GetEnvironmentVariable("PGDATABASE");

    connectionString = $"User ID={PGUSER};Password={PGPASSWORD};Host={PGHOST};Port={PGPORT};Database={PGDATABASE};";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("LOCALHOST");
}


builder.Services.AddDbContext<AstroContext>(options =>
    options.UseNpgsql(connectionString));

Action<IdentityOptions> identityOptions = m =>
{
    m.User.RequireUniqueEmail = true;
};

builder.Services.AddDefaultIdentity<Usuario>(identityOptions)
        .AddEntityFrameworkStores<AstroContext>()
        .AddErrorDescriber<LocalizedIdentityErrorDescriber>();

builder.Services.AddScoped<AstroService>();
builder.Services.AddScoped<PostagemService>();
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(24);

    options.LoginPath = "/SignIn/LogInView";
    options.AccessDeniedPath = "/SignIn/LogInView";
    options.SlidingExpiration = true;
});

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        // endpoints.MapRazorPages();
    });

app.Run();

MinimizeFootprint();


[DllImport("psapi.dll")]
static extern int EmptyWorkingSet(IntPtr hwProc);

static void MinimizeFootprint()
{
    EmptyWorkingSet(Process.GetCurrentProcess().Handle);
}