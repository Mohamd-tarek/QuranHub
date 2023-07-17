using QuranHub.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();

builder.AddCustomDatabase();

builder.AddCustomIdentity();

builder.AddCustomCaching();

builder.AddCustomCors();

builder.AddCustomAuthentication();

builder.Services.AddSignalR();

builder.AddCustomApplicationServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.MapFallbackToFile("index.html");


using (var scope = app.Services.CreateScope())
{
    await QuranSeedData.SeedDatabaseAsync(scope.ServiceProvider);

    await IdentitySeedData.SeedDatabaseAsync(scope.ServiceProvider);

   // await VideoSeedData.SeedDatabaseAsync(scope.ServiceProvider);
}

app.Run();


