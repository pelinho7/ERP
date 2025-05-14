//contractors
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Contractors.Infrastructure;
using Contractors.Application;
using Contractors.API.Filters;
using Contractors.API.Extensions;
using Contractors.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
//using Contractors.API.Policies;
using Microsoft.AspNetCore.Authorization;
using Dictionaries.Enums;
using Policies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<TokenDataFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.AddAppAuthetication();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
var permissionModuleService = builder.Configuration["PermissionModuleService"];
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Read", policy =>
        {
            policy.Requirements.Add(new PermissionRequirement((int)ModulePermissionEnum.ContractorsModuleRead, permissionModuleService));
        });
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Write", policy =>
    {
        policy.Requirements.Add(new PermissionRequirement((int)ModulePermissionEnum.ContractorsModuleWrite, permissionModuleService));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
//app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<ContractorContext>((context, services) => { });

app.Run();

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<TokenDataFilter>();
//});
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddApplicationServices();
//builder.Services.AddInfrastructureServices(builder.Configuration);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "https://localhost:5001"; //builder.Configuration["IdentityServer"];//"https://localhost:5001";
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false,
//            //ValidAudience = "https://localhost:4200",
//    };
//        options.IncludeErrorDetails = true;
//    });

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
////app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:8020"));
////app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

////app.MigrateDatabase<ProductContext>((context, services) => { });

//app.Run();
