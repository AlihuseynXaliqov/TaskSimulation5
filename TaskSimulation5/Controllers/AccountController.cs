using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskSimulation5.Models;
using TaskSimulation5.ModelViews;

namespace TaskSimulation5.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> role;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> role)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.role = role;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            AppUser appUser = new AppUser()
            {
                Name = vm.Name,
                Email = vm.Email,
                UserName = vm.Username
            };
/*            await userManager.AddToRoleAsync(appUser, Roles.Admin.ToString());*/
            var result = await userManager.CreateAsync(appUser, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(vm);
            }

            return RedirectToAction("Login");


        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var newUser = await userManager.FindByEmailAsync(vm.EmailOrUsername) ??
                await userManager.FindByNameAsync(vm.EmailOrUsername);
            if (newUser == null)
            {
                ModelState.AddModelError("", "Melumatlar duzgun deyil");
                return View(vm);
            }
            var result = await signInManager.CheckPasswordSignInAsync(newUser, vm.Password, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Melumatlar duzgun deyil");
                return View(vm);
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Az sonra yeniden yoxla");
                return View(vm);
            }
            await signInManager.SignInAsync(newUser, true);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                await role.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString()
                });
            };
            return RedirectToAction("Index", "Home");

        }
    }
}