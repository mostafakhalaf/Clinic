using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebUI.Helper;

var builder = WebApplication.CreateBuilder(args);
// get appsettings values 
IConfiguration configuration = builder.Configuration;
JWTConfigrations.SetTokenSecret(configuration["JWT:Secret"]);
DatabaseConfigrations.SetConnectionString(configuration["ConnectionStrings:DBConnectionString"]);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();
// adding jwt service
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTConfigrations.TokenSecret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };

});
// adding EntityFramework DBContext Service using ( SQL )
//builder.Services.AddDbContext<>(option => option.UseSqlServer(DatabaseConfigrations.ConnectionString));

// Repositories Services
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
