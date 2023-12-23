using Insurrance.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Insurrance.Controllers
{
   
    public class AccountController : Controller
    {
        private UserManager<AppUserViewModel> _user;
        private SignInManager<AppUserViewModel> _Signin;
        private RoleManager<IdentityRole> _RoleManager;

        public AccountController(UserManager<AppUserViewModel> user, SignInManager<AppUserViewModel> signin, RoleManager<IdentityRole> roleManager)
        {
            _user = user;
            _Signin = signin;
            _RoleManager = roleManager;
        }
      

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                

                var Result = await _Signin.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
                if (Result.Succeeded)
                {
                    return RedirectToAction("index", "Home");

                }

                ModelState.AddModelError("", "Invalid User Or Password");

                return View(model);

            }
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await _Signin.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


       

    }
    }


