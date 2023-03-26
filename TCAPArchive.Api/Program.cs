using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using TCAPArchive.Api.Models;
using TCAPArchive.Shared.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(TCAPMappingProfile));
builder.Services.AddCors(options =>
{
	options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
         .AddEntityFrameworkStores<TCAPContext>()
         .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.Cookie.Name = ".AspNetCore.Identity.Application";
           options.ExpireTimeSpan = TimeSpan.FromDays(1);
           options.SlidingExpiration = false;
       });

builder.Services.AddDbContext<TCAPContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"), o => o.CommandTimeout(180)
));

builder.Services.AddScoped<ITCAPRepository, TCAPRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseAuthentication();
app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("Open");

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
