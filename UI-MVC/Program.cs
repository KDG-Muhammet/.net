using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreManagement.BL;
using StoreManagement.DAL;
using StoreManagement.DAL.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GameDbContext>(optionsBuilder => optionsBuilder.UseSqlite("Data Source=AppDatabase.sqlite"));
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();
builder.Services.AddControllersWithViews();
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GameDbContext>();

// web api: fix redirect 302 to 401/403
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin += redirectContext =>
    {
        if (redirectContext.Request.Path.StartsWithSegments("/api"))
            redirectContext.Response.StatusCode = 401; // UnAuthorized
        return Task.CompletedTask;
    };
});
var app = builder.Build();

 
using (var scope = app.Services.CreateScope())
{
    GameDbContext ctx = scope.ServiceProvider.GetRequiredService<GameDbContext>();
    if (ctx.CreateDatabase(dataBase: true ))
    {
        UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        IdentitySeeding(userManager, roleManager);
        DataSeeder.Seed(ctx);
    }
}


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

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void IdentitySeeding(UserManager<IdentityUser> userManager,  RoleManager<IdentityRole> roleManager)
{
    var users = new List<IdentityUser>()
    {
        new IdentityUser { UserName = "assassinCreed@example.com", Email = "assassinCreed@example.com" },
        new IdentityUser { UserName = "fifa21@example.com", Email = "fifa21@example.com" },
        new IdentityUser { UserName = "cyberpunk2077@example.com", Email = "cyberpunk2077@example.com" },
        new IdentityUser { UserName = "theWitcher3@example.com", Email = "theWitcher3@example.com" },
    };
    foreach (var user in users) userManager.CreateAsync(user, "Student1234!");
    
    var admin = new IdentityUser("admin@app.com")
    {
        Email = "admin@app.com"
    };
    userManager.CreateAsync(admin, "Password1!");
    
    //roles
     roleManager.CreateAsync(new IdentityRole("Admin"));
     roleManager.CreateAsync(new IdentityRole("User"));
     
     userManager.AddToRoleAsync(admin, "Admin");
     foreach (var identityUser in users) userManager.AddToRoleAsync(identityUser, "User"); 
}