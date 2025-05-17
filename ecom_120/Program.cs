using ecom_120.DataAccess.Data;
using ecom_120.DataAccess.Repository;
using ecom_120.DataAccess.Repository.IRepository;
using ecom_120.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// ✅ Connection string
var cs = builder.Configuration.GetConnectionString("con")
         ?? throw new InvalidOperationException("Connection string 'con' not found.");

// ✅ DB context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(cs));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ✅ Identity config
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ✅ MVC, Razor, and runtime compilation
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();

// ✅ Dependency injection
builder.Services.AddScoped<IunitofWork, unitofWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

// ✅ Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// ✅ Social login: Facebook
builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "571360732738154";
    options.AppSecret = "74712902effd5e3d27181ec3f7a6599b";
});

// ✅ Social login: Google
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "115932499276-0k2e982pnjgk3pb3mn0sdbp5gq24fhhv.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

// ✅ Session configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedAdminAsync(services); // add this line
}

// ✅ Error handling
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Session must come BEFORE Authentication/Authorization
app.UseSession();

app.UseAuthentication();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeSettings")["Secretkey"];
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
