using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    // Create a new role
    public async Task<IActionResult> CreateRole(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return BadRequest("Role name is required");

        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (roleExist)
            return BadRequest("Role already exists");

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (result.Succeeded)
            return Ok($"Role {roleName} created successfully");

        return BadRequest("Role creation failed");
    }

    // Assign a role to a user
    public async Task<IActionResult> AssignRoleToUser(UserManager<IdentityUser> userManager, string userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return BadRequest("User not found");

        var result = await userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
            return Ok($"Role {roleName} assigned to user {user.UserName}");

        return BadRequest("Role assignment failed");
    }

    // Other methods for managing roles...
}
