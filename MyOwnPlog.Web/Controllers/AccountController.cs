using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MyOwnPlog.Web.Models.ViewModel;
using System.Security.Permissions;

namespace MyOwnPlog.Web.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager )
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.username,
                    Email = registerViewModel.Email

                };
                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.password);

                if (identityResult.Succeeded)
                {

                    //assign this user to user role 

                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");


                    if (roleIdentityResult.Succeeded)
                    {

                        return RedirectToAction("Register");
                    }
                }
            }
            
            return View();
        }


        [HttpGet]

        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel { ReturnUrl = ReturnUrl };
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
         var signResult = await signInManager.PasswordSignInAsync(loginViewModel.username, loginViewModel.password, false, false);
            if(signResult != null && signResult.Succeeded)
            {

                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

			return View();
		}

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
		
	}
}
