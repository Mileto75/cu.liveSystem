using cu.liveSystem.blazor.Components;
using cu.liveSystem.blazor.Hubs;
using cu.liveSystem.blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<StatusService>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
//register hub
app.MapHub<StatusHub>("/statusHub");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
