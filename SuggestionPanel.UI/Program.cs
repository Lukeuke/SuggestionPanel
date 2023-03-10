using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Application.Services.Authentication;
using SuggestionPanel.Application.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using SuggestionPanel.Application.Services.SMTP;
using SuggestionPanel.Domain.Models.SMTP;

var builder = WebApplication.CreateBuilder(args);

/*
 * 
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionStr = $"Data Source={dbHost};Initial Catalog={dbName};ValueStreamResponsibility ID=SA;Password={dbPassword};TrustServerCertificate=true";

ServicePointManager.ServerCertificateValidationCallback +=
    new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });
 */

builder.Services.AddDbContext<ApplicationContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISMTPService, SMTPService>();

builder.Services.AddSingleton(builder.Configuration.GetSection("smtp").Get<Settings>());

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(o =>
{
    o.LoginPath = "/auth/login";
    o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
