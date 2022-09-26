using Serilog;
using Serilog.Formatting.Compact;
using WebApplication3.Models;

var builder = WebApplication
    .CreateBuilder(args);

builder.Logging.ClearProviders();
//builder.Logging.AddFilter((provider, category, logLevel)=>
//{ 
//    if(provider.Contains("ConsoleLoggerProvider")
//    && category.Contains("Controller")
//    && logLevel >= LogLevel.Error)
//    {
//        return true;
//    }
//    else if(provider.Contains("ConsoleLoggerProvider")
//    && category.Contains("Microsoft")
//    && logLevel >= LogLevel.Information)
//    {
//        return true;
//    }
//    else
//    {
//        return false;
//    }
//});

builder.Logging
    .AddConfiguration(builder.Configuration.GetSection("Logging"));

//builder.Logging.AddDebug();
//builder.Logging.AddConsole();
//builder.Logging.AddEventLog();

builder.Logging.AddSeq();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IMathService, MathService>();

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Debug(new RenderedCompactJsonFormatter())
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
