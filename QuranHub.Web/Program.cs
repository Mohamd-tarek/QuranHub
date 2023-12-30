using Microsoft.Extensions.Hosting;
using QuranHub.Web;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentUserName()
        .Enrich.WithProcessId()
        .Enrich.WithProcessName()
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Log/log.txt"))
        //.WriteTo.Seq("http://localhost:5341", Serilog.Events.LogEventLevel.Debug)
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        .ReadFrom.Configuration(context.Configuration);
});

builder.AddCustomDatabase();

builder.AddCustomIdentity();

builder.AddCustomCaching();

builder.AddCustomCors();

builder.AddCustomAuthentication();

builder.Services.AddSignalR();

builder.AddCustomApplicationServices();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseMiddleware<DebugMiddleWare>();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllerRoute( 
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapHub<NotificationHub>("/NotificationHub");

app.MapGet("/video/{filename}", (string filename) =>
{
    //Build the File Path.  
    string path = Path.Combine(app.Environment.WebRootPath, "files/") + filename + ".mp4";  // the video file is in the wwwroot/files folder  

    var filestream = System.IO.File.OpenRead(path);
    return Results.File(filestream, contentType: "video/mp4", fileDownloadName: filename, enableRangeProcessing: true);
});

app.MapFallbackToFile("index.html");


using (var scope = app.Services.CreateScope())
{
    await QuranSeedData.SeedDatabaseAsync(scope.ServiceProvider);

    await IdentitySeedData.SeedDatabaseAsync(scope.ServiceProvider);

    await VideoSeedData.SeedDatabaseAsync(scope.ServiceProvider);
}

app.Run();


