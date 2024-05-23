using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProtracV1.Data;
using ProtracV1.Models;
using Microsoft.AspNetCore.Authorization;

using ProtracV1.Areas.Identity.Data;


System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var encoding = System.Text.Encoding.GetEncoding(1252);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ProtracAppContext' not found.")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

////// changed to false for noemail confirmation
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
    
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();


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
                var user = await userManager.FindByNameAsync("shubh1");
                //var hasRole = await roleManager.RoleExistsAsync("Admin");
                //if (await userManager.IsInRoleAsync(user, "Admin")) { throw new NotSupportedException("o Admin Role Exception in Assigning Role The default UI requires a user store with email support.");}

                // Check if the user exists and is not already in the role
                if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
                {
                    
                    // Add the user to the Admin role                    
                    var result = await userManager.AddToRoleAsync(user, "Admin");
                    if (result.Succeeded)
                    {
                        // Role assigned successfully
                    }
                    else
                    {
                        // Role not assigned successfully
                      // throw new NotSupportedException("Role not Assigned Exception in Assigning Role The default UI requires a user store with email support.");
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new NotSupportedException("Exception in Assigning Role The default UI requires a user store with email support.");
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

/////app.UseHttpsRedirection();
/////app.UseStaticFiles();

/////app.UseRouting();

//////app.UseAuthorization();
/////app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();





app.Run();
