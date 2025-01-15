using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PowerTracker.Models;

public class AdminController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // Метод за добавяне на роли в базата данни
    public async Task<IActionResult> AddRoles()
    {
        string[] roleNames = { "Admin", "Trainee" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = roleName
                };
                await _roleManager.CreateAsync(role);
            }
        }

        return View("Admin/AddRoles"); // Показваме изгледа за успешно добавяне на роли
    }

    // Метод за присвояване на роля към потребител
    [HttpPost]
    public async Task<IActionResult> AssignRoleToUser(string userEmail, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            return Content("User not found!");
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
        {
            return Content($"Role {roleName} assigned to {userEmail}.");
        }

        return Content("Error assigning role.");
    }
}

