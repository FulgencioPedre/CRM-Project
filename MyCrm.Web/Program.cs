using Microsoft.EntityFrameworkCore;
using MyCrm.Web.Components;
using MyCrm.Web.Data;
using MyCrm.Web.Service;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory,
    WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot")
});
builder.WebHost.UseUrls("http://localhost:5050", "https://localhost:5001");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=asociacion.db"));

builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<ProjectService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    // Esto aplica las migraciones pendientes al abrir la app
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Solo abre el navegador si estamos ejecutando el programa publicado
if (!app.Environment.IsDevelopment())
{
    // Esto abre automáticamente tu navegador por defecto en la dirección del CRM
    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("http://localhost:5050") { UseShellExecute = true });
}


app.Run();
