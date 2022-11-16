using DevTools.Data;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<Session>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<EncodingService>();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddMemoryCache(options => 
    options.ExpirationScanFrequency = TimeSpan.FromDays(7));

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
var port = Environment.GetEnvironmentVariable("PORT");
app.Run(string.IsNullOrEmpty(port) ? null : $"http://0.0.0.0:{port}");
