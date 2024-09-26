using GameOfLife.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder
    .Services
    .AddSession()
    .AddHttpContextAccessor()
    .AddDistributedMemoryCache()
    .AddSingleton<GameOfLifeService>()
    .AddScoped<BoardService>()
    .AddScoped<SessionInfoService>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    _ = app.UseHsts();
}

app
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseSession()
    .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=GameOfLife}/{action=Index}/{id?}");

app.Run();