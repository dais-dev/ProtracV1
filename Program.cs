using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProtracV1.Data;
using ProtracV1.Models;
using Microsoft.AspNetCore.Authorization;

using ProtracV1.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ProtracAppContext' not found.")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
    
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                // Code to create and assign roles goes here
                

                string[] roleNames = { "Admin", "Manager", "User" };
                IdentityResult roleResult;         

                foreach (var roleName in roleNames)
                {          
                    // Create roles if they don't exist
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                // Find the user by their email or username
                var user = await userManager.FindByEmailAsync("shubhangi.kelkar12@gmail.com");

                // Check if the user exists and is not already in the role
                if (user != null && !await userManager.IsInRoleAsync(user, "Manager"))
                {
                    // Add the user to the Admin role
                    await userManager.AddToRoleAsync(user, "Manager");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }




    SeedData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();





app.Run();
