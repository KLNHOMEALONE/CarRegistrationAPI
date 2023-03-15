using CarRegistrationAPI.Context;
using CarRegistrationAPI.Data;
using CarRegistrationAPI.Extensions;
using CarRegistrationAPI.Repositories;
using DapperMigrations.Migrations;
using FluentMigrator.Runner;
using Identity.Dapper.Postgres;
using Identity.Dapper.Postgres.Models;
using Identity.Dapper.Postgres.Stores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddDefaultTokenProviders();
builder.Services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
builder.Services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
builder.Services.AddTransient<IDatabaseConnectionFactory>(provider => new PostgresConnectionFactory(builder.Configuration.GetConnectionString("PostgresCarRegistrationDbConnection")));

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<Database>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(config => config.AddPostgres().WithGlobalConnectionString(builder.Configuration.GetConnectionString("PostgresCarRegistrationDbConnection"))
    .ScanIn(Assembly.GetExecutingAssembly()).For.All()).AddLogging(cfg => cfg.AddFluentMigratorConsole());

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration)
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});


builder.Services.AddScoped<IOwnerRepository, OwnersRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificatesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.Run();
