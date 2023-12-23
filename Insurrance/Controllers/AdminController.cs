using Insurrance.Data;
using Insurrance.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace Insurrance.Controllers
{
    public class AdminController : Controller
    {

        private UserManager<AppUserViewModel> _user;
        private SignInManager<AppUserViewModel> _Signin;
        private RoleManager<IdentityRole> _RoleManager;
        private readonly AppDbContext _Context;   
        public AdminController(UserManager<AppUserViewModel> user, SignInManager<AppUserViewModel> signin, RoleManager<IdentityRole> roleManager, AppDbContext Context)
        {
            _user = user;
            _Signin = signin;
            _RoleManager = roleManager;
            _Context = Context; 
        }

        [Authorize]
        public IActionResult Register()
        {
            var Roles = _RoleManager.Roles;
            
            var RoleViewmodel = new List<UserinRoleViewmodel>();
            foreach (var item in Roles)
            {
                UserinRoleViewmodel allRoles = new UserinRoleViewmodel()
                {
                    RoleId = item.Id,
                    RoleName = item.Name,
                    IsSelected = false

                };
                RoleViewmodel.Add(allRoles);
            }

            var UserRoleModel = new UserRoleViewModel()
            {

                RegisterViewModel = null,
                UserRolesModel = RoleViewmodel

            };
            ViewBag.Teams= new SelectList(_Context.Teams, "TeamID", "Name");
            return View(UserRoleModel);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRoleViewModel userRole)
        {

            if (ModelState.IsValid)
            {
                AppUserViewModel user = new AppUserViewModel()
                {
                    Email = userRole.RegisterViewModel.Email,
                    UserName = userRole.RegisterViewModel.Email,
                    PhoneNumber = userRole.RegisterViewModel.PhoneNumber,
                    TeamID=userRole.RegisterViewModel.TeamID,
                    Level = userRole.RegisterViewModel.Level,   


                };
                var result = await _user.CreateAsync(user, userRole.RegisterViewModel.Password);
                if (result.Succeeded)
                {
                    var role = userRole.UserRolesModel.ToList();
                    for (int Count = 0; Count < role.Count; Count++)
                    {
                        string RoleName = role[Count].RoleName!;
                        bool AddRole = role[Count].IsSelected;
                        if (AddRole == true)
                        {
                            await _user.AddToRoleAsync(user, RoleName);
                        }
                    }

                    return RedirectToAction("UsersList", "Admin");
                }
                foreach (var err in result.Errors)
                {

                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(userRole);

            }
            return View(userRole);
        }

        public IActionResult CreateRole()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel Model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = Model.RoleName
                };
                var result = await _RoleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");

                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);

                }
                return View(Model);
            }


            return View(Model);
        }


        [HttpGet]

        public async Task<IActionResult> EditRole(string id)
        {
            var Role = await _RoleManager.FindByIdAsync(id);
            EditRole model = new EditRole
            {
                RoleID = Role.Id,
                RoleName = Role.Name

            };

           
            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> EditRole(EditRole role)
        {
            if (ModelState.IsValid)
            {
                var Data = await _RoleManager.FindByIdAsync(role.RoleID);
                Data!.Name = role.RoleName;
                var result = await _RoleManager.UpdateAsync(Data);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(role);
            }
            return View(role);

        }


        [HttpGet]

        public async Task<IActionResult> DeleteRole(string id)
        {

            var role = await _RoleManager.FindByIdAsync(id);
            await _RoleManager.DeleteAsync(role!);

            return RedirectToAction("RolesList");
        }

        public IActionResult RolesList()
        {

            return View(_RoleManager.Roles);
        }



        public IActionResult UsersList()
        {

            ViewBag.Teams = new SelectList(_Context.Teams, "TeamID", "Name");
            return View(_user.Users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {

            var RoleViewmodel = new List<UserinRoleViewmodel>();
            var userData = await _user.FindByIdAsync(id);
          
            var role = _RoleManager.Roles.ToList();

            var user = new RegisterViewModel()
            {
                Email = userData.Email,
                PhoneNumber = userData.PhoneNumber,
                Id = userData.Id,
                Level = userData.Level,
                TeamID = userData.TeamID,
                
            };

            for (int i = 0; i < role.Count(); i++)
            {

                var userinrole = await _user.IsInRoleAsync(userData, role[i].Name);
                if (userinrole == true)
                {
                    UserinRoleViewmodel allRoles = new UserinRoleViewmodel()
                    {
                        RoleId = role[i].Id,
                        RoleName = role[i].Name,
                        IsSelected = true

                    };
                    RoleViewmodel.Add(allRoles);
                }
                else
                {
                    UserinRoleViewmodel allRoles = new UserinRoleViewmodel()
                    {
                        RoleId = role[i].Id,
                        RoleName = role[i].Name,
                        IsSelected = false

                    };
                    RoleViewmodel.Add(allRoles);
                }

            }
            var UserRoleModel = new UserRoleViewModel()
            {

                RegisterViewModel = user,
                UserRolesModel = RoleViewmodel

            };
            ViewBag.Teams = new SelectList(_Context.Teams, "TeamID", "Name");
            return View(UserRoleModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(UserRoleViewModel userRole)
        {


            var User = await _user.FindByIdAsync(userRole.RegisterViewModel.Id);
            User.UserName = userRole.RegisterViewModel.Email;
            
            User.Email = userRole.RegisterViewModel.Email;
            User.PhoneNumber = userRole.RegisterViewModel.PhoneNumber;
            User.TeamID = userRole.RegisterViewModel.TeamID;
            User.Level = userRole.RegisterViewModel.Level;
            await _user.UpdateAsync(User);


            var role = userRole.UserRolesModel.ToList();
            for (int i = 0; i < role.Count(); i++)
            {

                var userinrole = await _user.IsInRoleAsync(User, role[i].RoleName);
                if (userinrole == true)
                {
                    await _user.RemoveFromRoleAsync(User, role[i].RoleName);
                }

                if (role[i].IsSelected == true)
                {
                    await _user.AddToRoleAsync(User, role[i].RoleName);
                }
            }


            return RedirectToAction("UsersList");
        }


        [HttpGet]
        public IActionResult Config() 
        {
            
            var config=_Context.Configurations.FirstOrDefault();

            return View(config); 
        }

        [HttpPost]
        public IActionResult Config(Insurrance.Models.Configuration model )
        {

            var config = _Context.Configurations.Where(x=>x.Id==model.Id).FirstOrDefault();
            if (config != null)
            {
              config.CompanyName =model.CompanyName;
              config.CompanyEmail =model.CompanyEmail;
              config.EmailPassword =model.EmailPassword;
              config.Send =model.Send;
              config.SMTP =model.SMTP;
              config.Port =model.Port;
              config.Update =model.Update;
              config.AddedCar =model.AddedCar;
              config.Accident =model.Accident;
              config.Wellcome =model.Wellcome;
              config.compensation =model.compensation;
                config.Path =model.Path;    
            
            
                _Context.Configurations.Update(config);
            }
            else
            {
                _Context.Configurations.Add(model);
            }
            _Context.SaveChanges();


            return RedirectToAction("Index", "Home");
        }


    }
}
