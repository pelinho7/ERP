//using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
/// <summary>
/// Author : zahid.cakici@gmail.com
/// Date : 17.07.2022
/// Version : 1.0.0
/// </summary>
var builder = WebApplication.CreateBuilder(args);

//Host.CreateDefaultBuilder(args)
//.ConfigureAppConfiguration((hostingContext, config) =>
//{
//    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
//})
//.ConfigureLogging((hostingContext, loggingbuilder) => {
//    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
//    loggingbuilder.AddConsole();
//    loggingbuilder.AddDebug();
//});

// Add services to the container.

//builder.Services.AddAuthentication("IdentityApiKey")
//    .AddJwtBearer("IdentityApiKey", options =>
//    {
//        options.Authority = "https://localhost:5001";
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false
//        };
//    });

builder.Services.AddOcelot();
//builder.Configuration.AddJsonFile($"ocelot.Local.json");

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

Host.CreateDefaultBuilder(args)
.ConfigureLogging((hostingContext, loggingbuilder) =>
{
    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingbuilder.AddConsole();
    loggingbuilder.AddDebug();
});

builder.Configuration.AddJsonFile($"ocelot.{environment}.json");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
});

app.UseOcelot().Wait();

app.MapControllers();

app.Run();
