using System.Reflection;
using DevTools.Server;
using DevTools.Server.Data;
using Google.Cloud.Firestore;

var db2 = new FirestoreDbBuilder() { ProjectId = "testing-366118" };
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(o =>
    o.InputFormatters.Insert(o.InputFormatters.Count, new TextPlainInputFormatter()));
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen(x =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAutoMapper(typeof(Program));

if (builder.Environment.IsDevelopment())
{
    db2.EmulatorDetection = Google.Api.Gax.EmulatorDetection.EmulatorOnly;
}

builder.Services.AddSingleton<FirestoreDb>(_ => db2.Build());
builder.Services.AddTransient<IFirestoreProvider, FirestoreProvider>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.UseSwagger();
app.UseSwaggerUI();

var port = Environment.GetEnvironmentVariable("PORT") ?? null;
app.Run(port == null ? null : $"http://0.0.0.0:{port}");