using Astromedia.Models;
using Astromedia.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AstroContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LOCALHOST")));
    
builder.Services.AddDefaultIdentity<Usuario>()
        .AddEntityFrameworkStores<AstroContext>();

builder.Services.AddScoped<AstroService>();
builder.Services.AddScoped<PostagemService>();

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
        endpoints.MapRazorPages();
    });

app.Run();
