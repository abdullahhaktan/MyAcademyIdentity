using EmailApp.Context;
using EmailApp.Entities;
using EmailApp.Validations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

    options.UseSqlServer(connectionString);
});

// Configure Identity with custom user and role types
builder.Services.AddIdentity<AppUser, AppRole>(config =>
{
    config.User.RequireUniqueEmail = true; // Enforce unique email addresses

}).AddEntityFrameworkStores<AppDbContext>()
.AddErrorDescriber<CustomErrorDescriber>(); // Add custom error descriptions

// Add MVC services
builder.Services.AddControllersWithViews();

// Configure application cookie settings
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "IdentityCookie";
    opt.LoginPath = "/Login/Index"; // Redirect to login page when unauthorized
});

// Configure session state
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30); // Session expires after 30 minutes
    opt.Cookie.HttpOnly = true; // Prevent JavaScript access to session cookie
    opt.Cookie.IsEssential = true; // Cookie is essential for application functionality
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Use custom error page in production
    app.UseHsts(); // Enable HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession(); // Enable session middleware

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map static assets (CSS, JS, images)
app.MapStaticAssets();

// Configure default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();