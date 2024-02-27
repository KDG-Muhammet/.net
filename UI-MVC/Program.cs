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
        new IdentityUser { UserName = "user1@example.com", Email = "user1@example.com" },
        new IdentityUser { UserName = "user2@example.com", Email = "user2@example.com" },
        new IdentityUser { UserName = "user3@example.com", Email = "user3@example.com" },
        new IdentityUser { UserName = "user4@example.com", Email = "user4@example.com" },
        new IdentityUser { UserName = "user5@example.com", Email = "user5@example.com" },
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