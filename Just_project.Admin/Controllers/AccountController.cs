using Just_project.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Just_project.Admin.Controllers
{
    //public class AccountController : Controller
    //{
    //    private UserManager<AppUser> _userManager;
    //    private SignInManager<AppUser> _signInManager;
    //    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    //    {
    //        _userManager = userManager;
    //        _signInManager = signInManager;

    //    }
    //    public IActionResult Login()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Login(SignInModel sign)
    //    {
    //        AppUser appUser = await _userManager.FindByEmailAsync(sign.Username);
    //        if (appUser != null)
    //        {
    //            var result = await _signInManager.PasswordSignInAsync(appUser, sign.Password, false, false);
    //        }

    //        return RedirectToAction("Index", "Home");
    //    }

    //    public async Task<IActionResult> Logout()
    //    {
    //        await _signInManager.SignOutAsync();
    //        return RedirectToAction("Index", "Home");
    //    }
    //}

    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    return LocalRedirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
