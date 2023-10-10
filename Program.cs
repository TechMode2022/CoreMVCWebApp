using CoreMVCWebApp.Filters;
using CoreMVCWebApp.Service;
using Serilog;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews(config => config.Filters.Add(typeof(CustomExceptionFIlter)));

builder.Services.AddTransient<IHelloWorldService,HelloWorldService>();
builder.Services.AddTransient<ICalculatorService,CalculatorService>();

builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseSerilog((context, Configuration) => Configuration.ReadFrom.Configuration(context.Configuration));
//builder.Services.AddSingleton<ICocnfiguration>(Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();

app.UseSwaggerUI(c =>
{

});

app.UseRouting();

app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
