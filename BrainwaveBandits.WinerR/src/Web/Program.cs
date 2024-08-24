using BrainwaveBandits.WinerR.Infrastructure.Data;
using BrainwaveBandits.WinerR.Web.Tasks.ImportWine;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });


app.MapEndpoints();

app.UseHangfireDashboard();

var isRunningInNSwag = Environment.GetEnvironmentVariable("NSwagRunning") == "true";

if (!isRunningInNSwag)
{
    RecurringJob.AddOrUpdate<ImportWineTask>(
        "Import Wines",
        task => task.ExecuteAsync(),
        Cron.Daily);
}

app.Run();

public partial class Program { }
