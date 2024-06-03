using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProtracV1.Models;
using ProtracV1.Data;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProtracV1.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class AdminActionsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminActionsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
 
        }

        // GET: AdminActions
        public async Task<IActionResult> Index()
        {
        //    List<IdentityUser>  users = await _userManager.Users.ToListAsync();
        // return View(users);     
         //   return View();

         //   var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<UserRolesViewModel>();
            List<IdentityUser>  users = await _userManager.Users.ToListAsync();
                  
        
            foreach (IdentityUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.UserName = user.UserName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
        
            return View(userRolesViewModel);
        }

        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }


    

        public async Task<IActionResult> ListUsers()
        {
           List<IdentityUser>  users = await _userManager.Users.ToListAsync();
        return View(users);     

           // return View(await _context.JobStartForm.ToListAsync());
           //  return View();
        }

    }
}      